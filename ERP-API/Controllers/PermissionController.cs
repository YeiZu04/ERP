﻿using ERP_API.DTOs;
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
        var result = await _permissionService.CreatePermission(reqPermissionDto);

        if (result.Success)
        {
            return Ok(new { message = "Permiso creado exitosamente", permission = result.Data });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }

    // GET: api/Permission/list
    [HttpGet("list")]
    public async Task<IActionResult> ListPermissions()
    {
        var result = await _permissionService.ListPermissions();

        if (result.Success)
        {
            return Ok(new { message = "Lista de permisos obtenida exitosamente", permissions = result.Data });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }

    // PUT: api/Permission/update/{id}
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePermission(int id, [FromBody] ReqPermissionDto reqPermissionDto)
    {
        var result = await _permissionService.UpdatePermission(id, reqPermissionDto);

        if (result.Success)
        {
            return Ok(new { message = "Permiso actualizado exitosamente", permission = result.Data });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }

    // DELETE: api/Permission/delete/{id}
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePermission(int id)
    {
        var result = await _permissionService.DeletePermission(id);

        if (result.Success)
        {
            return Ok(new { message = "Permiso eliminado exitosamente" });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }
}
