using ERP_API.DTOs;
using ERP_API.Services;
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

    [HttpPost]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployee Employee)
    {
        await _employeeService.RegisterEmployeeAsync(Employee);
        return Ok();
    }
}
