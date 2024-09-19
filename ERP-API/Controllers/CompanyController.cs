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
        var result = await _companyService.ListCompanies();

        if (result.Success)
        {
            return Ok(new { message = "Lista de compañías obtenida exitosamente", companies = result.Data });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }


    // POST: api/Company/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        var result = await _companyService.CreateCompany(reqCompanyDto);

        if (result.Success)
        {
            return Ok(new { message = "Compañía creada exitosamente", company = result.Data });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }

    // PUT: api/Company/update/{id}
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCompany( [FromBody] ReqCompanyDto reqCompanyDto)
    {
        var result = await _companyService.UpdateCompany( reqCompanyDto);

        if (result.Success)
        {
            return Ok(new { message = "Compañía actualizada exitosamente", company = result.Data });
        }
        else if (result.ErrorCode == ErrorCode.NotFound)
        {
            return NotFound(new { message = result.ErrorMessage });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }

    // DELETE: api/Company/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        var result = await _companyService.DeleteCompany(reqCompanyDto);

        if (result.Success)
        {
            return Ok(new { message = result.Data });
        }
        else if (result.ErrorCode == ErrorCode.NotFound)
        {
            return NotFound(new { message = result.ErrorMessage });
        }
        else
        {
            return StatusCode(500, new { message = result.ErrorMessage });
        }
    }
}

