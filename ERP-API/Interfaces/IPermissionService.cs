// Interfaces/IPermissionService.cs
using ERP_API.DTOs;
using static ERP_API.Services.Tools.Api_Response;

namespace ERP_API.Interfaces
{
    public interface IPermissionService
    {
        Task<ApiResponse<ResPermissionDto>> CreatePermission(ReqPermissionDto reqPermissionDto);
        Task<ApiResponse<List<ResPermissionDto>>> ListPermissions();
        Task<ApiResponse<ResPermissionDto>> UpdatePermission(ReqPermissionDto reqPermissionDto);
        Task<ApiResponse<string>> DeletePermission(ReqPermissionDto reqPermissionDto);
    }
}
