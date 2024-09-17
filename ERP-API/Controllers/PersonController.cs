using ERP_API.DTOs;
using ERP_API.Services;
using ERP_API.Services.Tools;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    // GET: api/Person/list/
    [HttpGet("list")]
    public async Task<IActionResult> ListPerson()
    {
        var result = await _personService.ListPerson();

        if (result.Success)
        {
            return Ok(new
            {
                message = "Lista de personas obtenida exitosamente",
                people = result.Data
            });
        }
        else
        {
            return result.ErrorCode switch
            {
                Api_Response.ErrorCode.NotFound => NotFound(new { message = result.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = result.ErrorMessage }),
                _ => StatusCode(500, new { message = result.ErrorMessage })
            };
        }
    }

    // PUT: api/Person/update
    [HttpPut("update")]
    public async Task<IActionResult> UpdatePerson([FromBody] ResPersonDto resPersonDto)
    {
        var result = await _personService.UpdatePerson(resPersonDto);

        if (result.Success)
        {
            return Ok(new
            {
                message = "Persona actualizada exitosamente"
            });
        }
        else
        {
            return result.ErrorCode switch
            {
                Api_Response.ErrorCode.NotFound => NotFound(new { message = result.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = result.ErrorMessage }),
                _ => StatusCode(500, new { message = result.ErrorMessage })
            };
        }
    }

    // DELETE: api/Person/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePerson([FromBody] ResPersonDto resPersonDto)
    {
        var result = await _personService.DeletePerson(resPersonDto);

        if (result.Success)
        {
            return Ok(new
            {
                message = "Persona eliminada exitosamente"
            });
        }
        else
        {
            return result.ErrorCode switch
            {
                Api_Response.ErrorCode.NotFound => NotFound(new { message = result.ErrorMessage }),
                Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = result.ErrorMessage }),
                _ => StatusCode(500, new { message = result.ErrorMessage })
            };
        }
    }
}
 