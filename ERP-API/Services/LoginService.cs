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
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BearerCode _bearerCode;
        private readonly PasswordHash _passwordHash;

        public LoginService(ERPDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, BearerCode bearerCode, PasswordHash passwordHash )
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _bearerCode = bearerCode;
            _passwordHash = passwordHash;
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
            if (responseJWT.Success == true)
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
