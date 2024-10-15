using ERP_API.DTOs;

namespace ERP_API.Interfaces
{
    public interface IEmployeeService
    {

        Task <string> RegisterEmployee(ReqEmployeeDto ReqEmployeeDto);
    }
}
