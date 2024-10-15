using ERP_API.DTOs;
using ERP_API.Services.Tools;


namespace ERP_API.Interfaces
{
    public interface ILoginService
    {
        Task <string> Authenticate(LoginDto loginDto);
        Task <string> Logout();
        Task <string> RecoveryPassword(RecoveryPasswordDto recoveryPasswordDto);
        Task <string> ChangePassword(ChangePasswordDto changePassword);
 
    }
}
