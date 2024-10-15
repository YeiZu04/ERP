using ERP_API.DTOs;
using ERP_API.Models;
using ERP_API.Services.Tools;
using Microsoft.EntityFrameworkCore;
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

        public LoginService(ERPDbContext context, SendEmail sendEmail, IConfiguration configuration, RandomGenerator randomGenerator, BearerCode bearerCode, PasswordHash passwordHash)
        {
            _context = context;
            _bearerCode = bearerCode;
            _passwordHash = passwordHash;
            _randomGenerator = randomGenerator;
            _configuration = configuration;
            _emailSend = sendEmail;
        }

        public async Task<string> Authenticate(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.IdPersonFkNavigation)
                .ThenInclude(p => p.IdCompanyFkNavigation)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.IdRoleFkNavigation)
                .FirstOrDefaultAsync(u =>
                    u.NameUser == loginDto.Username &&
                    u.IdPersonFkNavigation.IdCompanyFkNavigation.CodeCompany == loginDto.CodeCompany);

            if (user == null || !_passwordHash.VerifyPassword(user.PasswordUser, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Contraseña o usuario incorrectos.");
            }

            // Verificamos si el estado de la persona es inactivo
            if (user.IdPersonFkNavigation.StatePerson != 1)
            {
                throw new InvalidOperationException("Empleado en estado inactivo.");
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

            return jwtToken;
        }

        public async Task<string> Logout()
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            _context.Sessions.Remove(responseJWT.Data);
            await _context.SaveChangesAsync();

            return "Sesión cerrada exitosamente.";
        }

        public async Task<string> RecoveryPassword(RecoveryPasswordDto recoveryPasswordDto)
        {
            var user = await _context.Users
                .Include(u => u.IdPersonFkNavigation)
                .ThenInclude(p => p.IdCompanyFkNavigation)
                .FirstOrDefaultAsync(u =>
                    u.NameUser == recoveryPasswordDto.UserName &&
                    u.IdPersonFkNavigation.IdentificationPerson == recoveryPasswordDto.IdentificationPerson &&
                    u.IdPersonFkNavigation.EmailPerson == recoveryPasswordDto.Email &&
                    u.IdPersonFkNavigation.IdCompanyFkNavigation.CodeCompany == recoveryPasswordDto.CodeCompany);

            if (user == null)
            {
                throw new KeyNotFoundException("Los datos no coinciden con algún usuario.");
            }

            // Generar nueva contraseña y actualizar usuario
            var randomPassword = _randomGenerator.GenerateRandomPassword();
            var newPassword = _passwordHash.HashPassword(randomPassword);
            user.PasswordUser = newPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            await _emailSend.SendEmailAsync(recoveryPasswordDto.Email, "Se ha cambiado tu contraseña", "Tu nueva contraseña es: " + randomPassword + ". Te recomendamos cambiarla.");

            return "Tu nueva contraseña fue enviada a tu correo.";
        }

        public async Task<string> ChangePassword(ChangePasswordDto changePassword)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == responseJWT.Data.IdUserFk);
            if (user == null || !_passwordHash.VerifyPassword(user.PasswordUser, changePassword.Password))
            {
                throw new UnauthorizedAccessException("Contraseña actual incorrecta.");
            }

            var newPassword = _passwordHash.HashPassword(changePassword.NewPassword);
            user.PasswordUser = newPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            await CloseExistingSessionAsync(user.IdUser);

            return "Tu contraseña ha sido cambiada.";
        }

        private async Task CloseExistingSessionAsync(int userId)
        {
            var existingSession = await _context.Sessions.FirstOrDefaultAsync(s => s.IdUserFk == userId);

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
