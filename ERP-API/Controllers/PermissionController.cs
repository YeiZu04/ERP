using ERP_API.DTOs;
using ERP_API.Services;
using ERP_API.Services.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly PermissionService _permissionService;

    public PermissionController(PermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // POST: api/Permission/create
    [HttpPost("create")]
    public async Task<IActionResult> CreatePermission([FromBody] ReqPermissionDto reqPermissionDto)
    {
        var response = await _permissionService.CreatePermission(reqPermissionDto);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return response.ErrorCode switch
            {
                Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                _ => StatusCode(500, new { message = response.ErrorMessage })
            };
        }
    }

    // GET: api/Permission/list
    [HttpGet("list")]
    public async Task<IActionResult> ListPermissions()
    {
        var response = await _permissionService.ListPermissions();

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return response.ErrorCode switch
            {
                Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                _ => StatusCode(500, new { message = response.ErrorMessage })
            };
        }
    }

    // PUT: api/Permission/update/{id}
    [HttpPut("update")]
    public async Task<IActionResult> UpdatePermission( [FromBody] ReqPermissionDto reqPermissionDto)
    {
        var response = await _permissionService.UpdatePermission( reqPermissionDto);

        if (response.Success)
        {
            return Ok( response);
        }
        else
        {
            return response.ErrorCode switch
            {
                Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                _ => StatusCode(500, new { message = response.ErrorMessage })
            };
        }
    }

    // DELETE: api/Permission/delete/{id}
    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePermission([FromBody] ReqPermissionDto reqPermissionDto)
    {
        var response = await _permissionService.DeletePermission(reqPermissionDto);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return response.ErrorCode switch
            {
                Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                _ => StatusCode(500, new { message = response.ErrorMessage })
            };
        }
    }
}
