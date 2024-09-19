using ERP_API.DTOs;
using ERP_API.Models;
using ERP_API.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERP_API.Services
{
    

    public class LoginService 
    {
        private readonly ERPDbContext _context;      
        private readonly BearerCode _bearerCode;
        private readonly IConfiguration _configuration;
        private readonly PasswordHash _passwordHash;
        private readonly RandomGenerator _randomGenerator;
        private readonly SendEmail _emailSend;

        public LoginService(ERPDbContext context, SendEmail sendEmail,IConfiguration configuration, RandomGenerator randomGenerator, BearerCode bearerCode, PasswordHash passwordHash )
        {
            _context = context;
            _bearerCode = bearerCode;
            _passwordHash = passwordHash;
            _randomGenerator = randomGenerator;
            _configuration = configuration;
            _emailSend = sendEmail;
        }

        public async Task<Api_Response.ApiResponse<string>> Authenticate(LoginDto loginDto)
        {

            try
            {
                var user = await _context.Users
                .Include(u => u.IdPersonFkNavigation)
                .ThenInclude(p => p.IdCompanyFkNavigation)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.IdRoleFkNavigation)
                .FirstOrDefaultAsync(u =>
                u.NameUser == loginDto.Username &&
                u.IdPersonFkNavigation.IdCompanyFkNavigation.CodeCompany == loginDto.CodeCompany);

                if (user == null && _passwordHash.VerifyPassword(user.PasswordUser, loginDto.Password))
                {
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.InvalidInput,
                        ErrorMessage = "Contraseña o usuario incorrectos "
                    };
                }

                // Verificamos si el estado de la persona es inactivo
                if (user.IdPersonFkNavigation.StatePerson != 1)
                {
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.InvalidInput,
                        ErrorMessage = "Empleado en estado inactivo"
                    };
                }

                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, user.NameUser)
                };

                foreach (var userRole in user.UserRoles)
                {
                    if (userRole.IdRoleFkNavigation != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.IdRoleFkNavigation.TypeRole));
                    }
                }

                var jwtToken = tokenStruct(claims, loginDto.Created_at.AddHours(1));
                var session = new Session
                {
                    TokenSession = jwtToken,
                    CreationDateSession = loginDto.Created_at,
                    ExpirationDateSession = loginDto.Created_at.AddHours(1),
                    IdUserFk = user.IdUser,
                    StatusSession = 1
                };

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();
                
                return new Api_Response.ApiResponse<string>
                {
                    Success = true,
                    Data = jwtToken
                };


            }
            catch (Exception ex)
            {
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
                    ErrorMessage = ex.Message
                };

            }
        }

        public async Task<Api_Response.ApiResponse<string>> Logout()
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (responseJWT.Success)
            {
                _context.Sessions.Remove(responseJWT.Data);
                await _context.SaveChangesAsync();
                return new Api_Response.ApiResponse<string>
                {
                    Success = true,
                    Data = "Session cerrada existosamente"
                };                   
            }
            else
            {
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = responseJWT.ErrorCode,
                    ErrorMessage = responseJWT.ErrorMessage
                };

            }
        }

        public async Task<Api_Response.ApiResponse<string>> RecoveryPassword(RecoveryPasswordDto recoveryPasswordDto)
        {
            try
            {


                var user = await _context.Users
                .Include(u => u.IdPersonFkNavigation) // Incluye la relación con la tabla Person
                .ThenInclude(p => p.IdCompanyFkNavigation) // Luego incluye la relación con la tabla Company
                .FirstOrDefaultAsync(u =>
                    u.NameUser == recoveryPasswordDto.UserName && // Filtro por nombre de usuario
                    u.IdPersonFkNavigation.IdentificationPerson == recoveryPasswordDto.IdentificationPerson && // Filtro por identificación de la persona
                    u.IdPersonFkNavigation.EmailPerson == recoveryPasswordDto.Email && // Filtro por email de la persona
                    u.IdPersonFkNavigation.IdCompanyFkNavigation.CodeCompany == recoveryPasswordDto.CodeCompany); // Filtro por código de la empresa

                if (user == null)
                {
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.NotFound,
                        ErrorMessage = "Los datos no coinciden con algun usuario "
                    };
                }

                // 2. Registrar el Usuario
                var ramdomPaswword = _randomGenerator.GenerateRandomPassword();
                var newPassword = _passwordHash.HashPassword(ramdomPaswword);
                user.PasswordUser = newPassword;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                await _emailSend.SendEmailAsync(recoveryPasswordDto.Email, "Se ha cambiado tu contraseña", "Tu password será:  " + ramdomPaswword + " Te recomendamos cambiarla");
                return new Api_Response.ApiResponse<string>
                {
                    Success = true,
                    Data = "Tu nueva contraña fue enviada a tu correo"
                };
            }
            catch (Exception ex)
            {
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
                    ErrorMessage = "Error al recuperar contraseña:" + ex.Message
                };
            }
        }
        public async Task<Api_Response.ApiResponse<string>> ChangePassword(ChangePasswordDto changePassword)
        {

            try
            {

                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }
                var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                u.IdUser == responseJWT.Data.IdUserFk);

                if (user == null && _passwordHash.VerifyPassword(user.PasswordUser, changePassword.Password))
                {
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.InvalidInput,
                        ErrorMessage = "Contraseña  incorrecta "
                    };
                }


                var newPassword = _passwordHash.HashPassword(changePassword.NewPassword);
                user.PasswordUser = newPassword;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                await CloseExistingSessionAsync(user.IdUser);
                return new Api_Response.ApiResponse<string>
                {
                    Success = true,
                    Data = "Tu contraseña ha sido cambiada"
                };

            }

            catch (Exception ex)
            {
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
                    ErrorMessage = "Error al cambio de contraseña:" + ex.Message
                };

            }
           
        }

        private async Task CloseExistingSessionAsync(int userId)
        {
            var existingSession = await _context.Sessions
                .FirstOrDefaultAsync(s => s.IdUserFk == userId);

            if (existingSession != null)
            {
                _context.Sessions.Remove(existingSession);
                await _context.SaveChangesAsync();
            }
        }

        public string tokenStruct(List<Claim> claims, DateTime expire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;

        }
    }
}
