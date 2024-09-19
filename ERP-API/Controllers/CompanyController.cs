using ERP_API.DTOs;
using ERP_API.Services;
using Microsoft.AspNetCore.Mvc;
using ERP_API.Services.Tools;
using static ERP_API.Services.Tools.Api_Response;


[ApiController]
[Route("api/[controller]")]

public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    // GET: api/Company/list
    [HttpGet("list")]
    public async Task<IActionResult> ListCompanies()
    {
        var response = await _companyService.ListCompanies();

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


    // POST: api/Company/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        var response = await _companyService.CreateCompany(reqCompanyDto);

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

    // PUT: api/Company/update/{id}
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCompany( [FromBody] ReqCompanyDto reqCompanyDto)
    {
        var response = await _companyService.UpdateCompany( reqCompanyDto);

        if (response.Success)
        {
            return Ok(response);
        }else
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

    // DELETE: api/Company/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        var response = await _companyService.DeleteCompany(reqCompanyDto);

        if (response.Success)
        {
            return Ok( response);
        }else
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

