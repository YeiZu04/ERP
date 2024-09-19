using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Models;


namespace ERP_API.Utilities
{
    public class CompanyMapperProfile : Profile 
    {
        public CompanyMapperProfile()
        {
            CreateMap<Company, ReqCompanyDto>();
            CreateMap<ReqCompanyDto, Company>();

            CreateMap<Company, ResCompanyDto>();
            CreateMap<ResCompanyDto, Company>();
        }
    }
}
