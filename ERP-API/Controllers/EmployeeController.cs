using ERP_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP_API.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege todos los métodos de este controlador
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterEmployee([FromBody] ReqEmployeeDto employeeDto)
    {
        // Llama al servicio para registrar al empleado
        var result = await _employeeService.RegisterEmployee(employeeDto);
        
        // Devuelve una respuesta 200 OK con el resultado exitoso
        return Ok(new { message = result });

    }
}
