using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Models;

namespace ERP_API.Utilities

{
    public class PersonMapperProfile : Profile
    {
       public PersonMapperProfile() {

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto , Person>();
        }
    }
}
