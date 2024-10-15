// Interfaces/IPermissionService.cs
using ERP_API.DTOs;

namespace ERP_API.Interfaces
{
    public interface IPermissionService
    {
        Task <ResPermissionDto> CreatePermission(ReqPermissionDto reqPermissionDto);
        Task <List<ResPermissionDto>> ListPermissions();
        Task <ResPermissionDto> UpdatePermission(ReqPermissionDto reqPermissionDto);
        Task <string> DeletePermission(ReqPermissionDto reqPermissionDto);
    }
}
