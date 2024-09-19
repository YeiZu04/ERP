using AutoMapper;
using ERP_API.DTOs;
using ERP_API.Models;
using ERP_API.Services.Tools;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ERP_API.Services.Tools.Api_Response;

namespace ERP_API.Services
{
    public class PermissionService
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
        public async Task<ApiResponse<ResPermissionDto>> CreatePermission(ReqPermissionDto reqPermissionDto)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new ApiResponse<ResPermissionDto>{
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }
                var permission = _mapper.Map<Permission>(reqPermissionDto);
                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync();

                var resPermissionDto = _mapper.Map<ResPermissionDto>(permission);

                return new ApiResponse<ResPermissionDto>
                {
                    Success = true,
                    Data = resPermissionDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResPermissionDto>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al crear el permiso: {ex.Message}"
                };
            }
        }

        // Método para listar todos los permisos
        public async Task<ApiResponse<List<ResPermissionDto>>> ListPermissions()
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new ApiResponse<List<ResPermissionDto>>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }
                var permissions = await _context.Permissions.ToListAsync();
                var resPermissionList = _mapper.Map<List<ResPermissionDto>>(permissions);

                return new ApiResponse<List<ResPermissionDto>>
                {
                    Success = true,
                    Data = resPermissionList
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResPermissionDto>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al obtener la lista de permisos: {ex.Message}"
                };
            }
        }

        // Método para actualizar un permiso
        public async Task<ApiResponse<ResPermissionDto>> UpdatePermission(int id, ReqPermissionDto reqPermissionDto)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new ApiResponse<ResPermissionDto>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }
                var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.IdPermission == id);
                if (permission == null)
                {
                    return new ApiResponse<ResPermissionDto>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                        ErrorMessage = "Permiso no encontrado."
                    };
                }

                _mapper.Map(reqPermissionDto, permission);
                await _context.SaveChangesAsync();

                var resPermissionDto = _mapper.Map<ResPermissionDto>(permission);
                return new ApiResponse<ResPermissionDto>
                {
                    Success = true,
                    Data = resPermissionDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResPermissionDto>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al actualizar el permiso: {ex.Message}"
                };
            }
        }

        // Método para eliminar un permiso
        public async Task<ApiResponse<string>> DeletePermission(int id)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return  new ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }
                var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.IdPermission == id);
                if (permission == null)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                        ErrorMessage = "Permiso no encontrado."
                    };
                }

                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();

                return new ApiResponse<string>
                {
                    Success = true,
                    Data = "Permiso eliminado correctamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al eliminar el permiso: {ex.Message}"
                };
            }
        }
    }
}
