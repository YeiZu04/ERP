// Interfaces/ICompanyService.cs
using ERP_API.DTOs;
using ERP_API.Services.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ERP_API.Services.Tools.Api_Response;

namespace ERP_API.Interfaces
{
    public interface ICompanyService
    {
        Task<ApiResponse<ResCompanyDto>> CreateCompany(ReqCompanyDto reqCompanyDto);
        Task<ApiResponse<ResCompanyDto>> UpdateCompany(ReqCompanyDto reqCompanyDto);
        Task<ApiResponse<string>> DeleteCompany(ReqCompanyDto reqCompanyDto);
        Task<ApiResponse<List<ResCompanyDto>>> ListCompanies();
    }
}
