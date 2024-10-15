using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Interfaces;
using ERP_API.Models;
using ERP_API.Services.Tools;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_API.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ERPDbContext _context;
        private readonly IMapper _mapper;
        private readonly BearerCode _bearerCode;

        public PermissionService(ERPDbContext context, IMapper mapper, BearerCode bearerCode)
        {
            _context = context;
            _mapper = mapper;
            _bearerCode = bearerCode;
        }

        // Método para crear un permiso
        public async Task<ResPermissionDto> CreatePermission(ReqPermissionDto reqPermissionDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var permission = _mapper.Map<Permission>(reqPermissionDto);
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResPermissionDto>(permission);
        }

        // Método para listar todos los permisos
        public async Task<List<ResPermissionDto>> ListPermissions()
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var permissions = await _context.Permissions.ToListAsync();
            return _mapper.Map<List<ResPermissionDto>>(permissions);
        }

        // Método para actualizar un permiso
        public async Task<ResPermissionDto> UpdatePermission(ReqPermissionDto reqPermissionDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.IdPermission == reqPermissionDto.IdPermission);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permiso no encontrado.");
            }

            _mapper.Map(reqPermissionDto, permission);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResPermissionDto>(permission);
        }

        // Método para eliminar un permiso
        public async Task<string> DeletePermission(ReqPermissionDto reqPermissionDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.IdPermission == reqPermissionDto.IdPermission);
            if (permission == null)
            {
                throw new KeyNotFoundException("Permiso no encontrado.");
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return "Permiso eliminado correctamente.";
        }
    }
}
