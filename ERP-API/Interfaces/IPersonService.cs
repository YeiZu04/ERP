using ERP_API.DTOs;

namespace ERP_API.Interfaces
{
    public interface IPersonService
    {
        Task <ResPersonDto> UpdatePerson(ResPersonDto resPersonDto);
        Task <List<ResPersonDto>> ListPerson();
        Task <string> DeletePerson(ResPersonDto resPersonDto);
    }
}
