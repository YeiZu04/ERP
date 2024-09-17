using ERP_API.Models;
using Microsoft.EntityFrameworkCore;

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




        public async Task<Api_Response.ApiResponse<Session>> VerficationCode()
        {
            try
            {

                var token = _httpContextAccessor.HttpContext.Items["UserToken"]?.ToString();

                if (string.IsNullOrEmpty(token))
                    return new Api_Response.ApiResponse<Session>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.NotFound,
                        ErrorMessage = "Código JWT no encontrado."
                    };

                var session = await _Dbcontext.Sessions
                    .Include(s => s.IdUserFkNavigation)         // Incluir la navegación hacia User
                    .ThenInclude(u => u.IdPersonFkNavigation)   // Incluir la navegación hacia Person
                    .ThenInclude(p => p.IdCompanyFkNavigation)  // Incluir la navegación hacia Company
                    .FirstOrDefaultAsync(s => s.TokenSession == token);


                if (session == null)
                {
                    return new Api_Response.ApiResponse<Session>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.NotFound,
                        ErrorMessage = "Session no encontrada"
                    };
                }
                return new Api_Response.ApiResponse<Session>
                {
                    Success = true,
                    Data = session
                    
                };
            }
            catch (Exception ex)
            {

                return new Api_Response.ApiResponse<Session>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
                    ErrorMessage = "Error en la obtencion de JWT: "+ ex.Message
                };

            }
        }
    }
}
