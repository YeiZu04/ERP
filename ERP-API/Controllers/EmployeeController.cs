using ERP_API.DTOs;
using ERP_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege todos los métodos de este controlador
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterEmployee([FromBody] ReqEmployeeDto employeeDto)
    {
        // Llama al servicio para registrar al empleado
        var result = await _employeeService.RegisterEmployeeAsync(employeeDto);
        
        // Devuelve una respuesta 200 OK con el resultado exitoso
        return Ok(new { message = result });

    }
}
