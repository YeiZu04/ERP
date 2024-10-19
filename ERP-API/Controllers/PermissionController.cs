using ERP_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP_API.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege todos los métodos de este controlador
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // POST: api/Permission/create
    [HttpPost("create")]
    public async Task<IActionResult> CreatePermission([FromBody] ReqPermissionDto reqPermissionDto)
    {
        var permission = await _permissionService.CreatePermission(reqPermissionDto);
        return Ok(permission);
    }

    // GET: api/Permission/list
    [HttpGet("list")]
    public async Task<IActionResult> ListPermissions()
    {
        var permissions = await _permissionService.ListPermissions();
        return Ok(permissions);
    }

    // PUT: api/Permission/update
    [HttpPut("update")]
    public async Task<IActionResult> UpdatePermission([FromBody] ReqPermissionDto reqPermissionDto)
    {
        var permission = await _permissionService.UpdatePermission(reqPermissionDto);
        return Ok(permission);
    }

    // DELETE: api/Permission/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePermission([FromBody] ReqPermissionDto reqPermissionDto)
    {
        var result = await _permissionService.DeletePermission(reqPermissionDto);
        return Ok(new { message = result });
    }
}
