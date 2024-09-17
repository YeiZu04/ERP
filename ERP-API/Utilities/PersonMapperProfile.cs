using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Models;

namespace ERP_API.Utilities

{
    public class PersonMapperProfile : Profile
    {
       public PersonMapperProfile() {

            CreateMap<Person, ReqPersonDto>();
            CreateMap<ReqPersonDto , Person>();

            CreateMap<Person, ResPersonDto>();
            CreateMap<ResPersonDto, Person>();
        }
    }
}
