using ERP_API.DTOs;
using ERP_API.Services.Tools;
using static ERP_API.Services.Tools.Api_Response;

namespace ERP_API.Interfaces
{
    public interface IEmployeeService
    {

        Task<ApiResponse<string>> RegisterEmployee(ReqEmployeeDto ReqEmployeeDto);
    }
}
