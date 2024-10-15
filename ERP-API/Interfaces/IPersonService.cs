using ERP_API.DTOs;
using static ERP_API.Services.Tools.Api_Response;

namespace ERP_API.Interfaces
{
    public interface IPersonService
    {
        Task<ApiResponse<ResPersonDto>> UpdatePerson(ResPersonDto resPersonDto);
        Task<ApiResponse<List<ResPersonDto>>> ListPerson();
        Task<ApiResponse<string>> DeletePerson(ResPersonDto resPersonDto);
    }
}
