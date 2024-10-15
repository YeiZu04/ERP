// Interfaces/ICompanyService.cs
using ERP_API.DTOs;

namespace ERP_API.Interfaces
{
    public interface ICompanyService
    {
        Task <ResCompanyDto> CreateCompany(ReqCompanyDto reqCompanyDto);
        Task <ResCompanyDto> UpdateCompany(ReqCompanyDto reqCompanyDto);
        Task  DeleteCompany(ReqCompanyDto reqCompanyDto);
        Task <List<ResCompanyDto>> ListCompanies();
    }
}
