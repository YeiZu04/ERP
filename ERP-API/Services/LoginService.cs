using ERP_API.DTOs;
using ERP_API.Models;
using ERP_API.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ERP_API.Interfaces;
using System.Security.Claims;
using System.Text;

namespace ERP_API.Services
{
    

    public class LoginService : IlogginService
    {
        private readonly ERPDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BearerCode _bearerCode;

        public LoginService(ERPDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, BearerCode bearerCode )
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _bearerCode = bearerCode;
        }

        public async Task<Api_Response.ApiResponse<string>> Authenticate(LoginDto loginDTO)
        {

            try
            {
                var user = await _context.Users
                   .Include(u => u.IdPersonFkNavigation)
                   .ThenInclude(p => p.IdCompanyFkNavigation)  // Incluimos la navegación hacia Company
                   .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.IdRoleFkNavigation)
                   .FirstOrDefaultAsync(u =>
                       u.NameUser == loginDTO.Username &&
                       u.PasswordUser == loginDTO.Password &&
                       u.IdPersonFkNavigation.IdCompanyFkNavigation.CodeCompany == loginDTO.CodeCompany);  // Filtro por CodeCompany



                if (user == null)
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.InvalidInput,
                        ErrorMessage = "Contraseña o usuario incorrectos"
                    };

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

                var jwtToken = tokenStruct(claims, loginDTO.Created_at.AddHours(1));
                var session = new Session
                {
                    TokenSession = jwtToken,
                    CreationDateSession = loginDTO.Created_at,
                    ExpirationDateSession = loginDTO.Created_at.AddHours(1),
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

        public async Task<Api_Response.ApiResponse<string>> Logout(string codeJwt)
        {
            var responseJWT = await _bearerCode.VerficationCode(codeJwt);
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
