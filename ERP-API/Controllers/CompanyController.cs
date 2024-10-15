using ERP_API.DTOs;
using ERP_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege todos los métodos de este controlador
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
        var companies = await _companyService.ListCompanies();
        return Ok(companies);  // El middleware se encargará de los errores
    }

    // POST: api/Company/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        var company = await _companyService.CreateCompany(reqCompanyDto);
        return Ok(company);  // El middleware se encargará de los errores
    }

    // PUT: api/Company/update
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        var company = await _companyService.UpdateCompany(reqCompanyDto);
        return Ok(company);  // El middleware se encargará de los errores
    }

    // DELETE: api/Company/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCompany([FromBody] ReqCompanyDto reqCompanyDto)
    {
        await _companyService.DeleteCompany(reqCompanyDto);
        return Ok(new { message = "Compañía eliminada exitosamente" });  // El middleware se encargará de los errores
    }
}
