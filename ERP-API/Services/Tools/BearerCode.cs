using ERP_API.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace ERP_API.Services.Tools
{
    public class BearerCode
    {
        private readonly ERPDbContext _Dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BearerCode(IHttpContextAccessor httpContextAccessor, ERPDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _Dbcontext = dbContext;
        }

        public async Task<Session> VerficationCode()
        {
            // Obtener el token del encabezado Authorization
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Código JWT no encontrado en la petición.");
            }

            // Validar si el token es legible
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                throw new UnauthorizedAccessException("El formato del token JWT es inválido.");
            }

            // Buscar la sesión en la base de datos usando el token
            var session = await _Dbcontext.Sessions
                .Include(s => s.IdUserFkNavigation)         // Incluir la navegación hacia User
                .ThenInclude(u => u.IdPersonFkNavigation)   // Incluir la navegación hacia Person
                .ThenInclude(p => p.IdCompanyFkNavigation)  // Incluir la navegación hacia Company
                .FirstOrDefaultAsync(s => s.TokenSession == token && s.StatusSession == 1);  // Buscar sesión activa por token

            if (session == null)
            {
                throw new UnauthorizedAccessException("Sesión no encontrada o el token es inválido.");
            }

            return session;  // Devolver la sesión si se encuentra y está activa
        }
    }
}
