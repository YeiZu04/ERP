using ERP_API.DTOs;
using ERP_API.Services.Tools;

namespace ERP_API.Interfaces
{
    public interface IlogginService
    {
        Task<Api_Response.ApiResponse<string>> Authenticate(LoginDto loginDTO);
        Task<Api_Response.ApiResponse<string>> Logout(string codeJwt);
    }
}
