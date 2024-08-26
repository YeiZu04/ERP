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
    public async Task<IActionResult> RegisterEmployee(RegisterPersonDto personDto, RegisterUserDto userDto, RegisterEmployeeDto employeeDto, RegisterUserRoleDto userRoleDto, RegisterCurriculumDto curriculumDto)
    {
        await _employeeService.RegisterEmployeeAsync(personDto, userDto, employeeDto, userRoleDto, curriculumDto);
        return Ok();
    }
}
