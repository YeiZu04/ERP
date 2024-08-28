using ERP_API.DTOs;
using ERP_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERP_API.Services
{
    public interface ILogginService{
        Task<string> Authenticate(LogginDto loginDTO);
    }

    public class LogginService : ILogginService{
        private readonly ERPDbContext _context;
        private readonly IConfiguration _configuration;

        public LogginService(ERPDbContext context, IConfiguration configuration){
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Authenticate(LogginDto loginDTO){
            var user = await _context.Users
                .Include(u => u.IdPersonFkNavigation)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.IdRoleFkNavigation) // Incluye los roles a través de la propiedad de navegación
                .FirstOrDefaultAsync(u => u.NameUser == loginDTO.Username && u.PasswordUser == loginDTO.Password);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.NameUser)
            };

            // Añadir los roles a los claims
            foreach (var userRole in user.UserRoles){
                if (userRole.IdRoleFkNavigation != null){
                    claims.Add(new Claim(ClaimTypes.Role, userRole.IdRoleFkNavigation.TypeRole));
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            // Crear la nueva sesión
            var session = new Session{
                TokenSession = jwtToken,
                CreationDateSession = DateTime.Now,
                ExpirationDateSession = DateTime.Now.AddHours(1),
                IdUserFk = user.IdUser,
                StatusSession = 1
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return jwtToken;
        }



    }
}
