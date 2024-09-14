using ERP_API.DTOs;
using ERP_API.Services;
using ERP_API.Services.Tools;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployee employeeDto)
    {
        // Llama al servicio para registrar al empleado
        var result = await _employeeService.RegisterEmployeeAsync(employeeDto);

        // Verifica si la operación fue exitosa
        if (result.Success)
        {
            // Devuelve una respuesta 200 OK 
            return Ok(new
            {
                message = "Empleado registrado exitosamente",
                employeeId = result.Data
            });
        }
        else
        {
            // Devuelve una respuesta con el código de error adecuado
            return result.ErrorCode switch
          {
              Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = result.ErrorMessage }),
              Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = result.ErrorMessage }),
              Api_Response.ErrorCode.NotFound => NotFound(new { message = result.ErrorMessage }),
              Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = result.ErrorMessage }),
              _ => StatusCode(500, new { message = result.ErrorMessage })
          };
        }
    }
}