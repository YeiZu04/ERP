using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Models;

namespace ERP_API.Utilities
{
    public class EmployeeMapperProfile : Profile
    {

        public EmployeeMapperProfile()
        {

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
