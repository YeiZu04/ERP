using ERP_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP_API.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege todos los métodos de este controlador
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    // GET: api/Person/list/
    [HttpGet("list")]
    public async Task<IActionResult> ListPerson()
    {
        var people = await _personService.ListPerson();
        return Ok(people);
    }

    // PUT: api/Person/update
    [HttpPut("update")]
    public async Task<IActionResult> UpdatePerson([FromBody] ResPersonDto resPersonDto)
    {
        var person = await _personService.UpdatePerson(resPersonDto);
        return Ok(person);
    }

    // DELETE: api/Person/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePerson([FromBody] ResPersonDto resPersonDto)
    {
        var result = await _personService.DeletePerson(resPersonDto);
        return Ok(new { message = result });
    }
}
