using ERP_API.DTOs;
using ERP_API.Services.Tools;


namespace ERP_API.Interfaces
{
    public interface ILoginService
    {
        Task<Api_Response.ApiResponse<string>> Authenticate(LoginDto loginDto);
        Task<Api_Response.ApiResponse<string>> Logout();
        Task<Api_Response.ApiResponse<string>> RecoveryPassword(RecoveryPasswordDto recoveryPasswordDto);
        Task<Api_Response.ApiResponse<string>> ChangePassword(ChangePasswordDto changePassword);
 
    }
}
