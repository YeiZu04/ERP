using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Models;

namespace ERP_API.Utilities
{
    public class PermissionMapperProfile : Profile
    {
        public PermissionMapperProfile() {
            CreateMap<Permission, ReqPermissionDto>();
            CreateMap<ReqPermissionDto, Permission>();

            CreateMap<Permission, ResPermissionDto>();
            CreateMap<ResPermissionDto, Permission>();

        }
    }
}
