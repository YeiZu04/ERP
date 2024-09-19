using ERP_API.DTOs;
using ERP_API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ERP_API.Services.Tools.Api_Response;
using ERP_API.Services.Tools;


namespace ERP_API.Services
{
    public class CompanyService
    {
        private readonly ERPDbContext _context;
        private readonly IMapper _mapper;
        private readonly BearerCode _bearerCode;

        public CompanyService(ERPDbContext context, IMapper mapper , BearerCode bearerCode)
        {
            _context = context;
            _mapper = mapper;
            _bearerCode = bearerCode;
        }

        // CREATE
        public async Task<ApiResponse<ResCompanyDto>> CreateCompany(ReqCompanyDto reqCompanyDto)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new ApiResponse<ResCompanyDto>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }
                var company = _mapper.Map<Company>(reqCompanyDto);
                company.StatusCompany = 1;

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                var resCompanyDto = _mapper.Map<ResCompanyDto>(company);

                return new ApiResponse<ResCompanyDto>
                {
                    Success = true,
                    Data = resCompanyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResCompanyDto>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al crear la compañía: {ex.Message}"
                };
            }
        }

        // READ
        public async Task<ApiResponse<ResCompanyDto>> GetCompany(int id)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();

                if (responseJWT.Success == false)
                {
                    return new ApiResponse<ResCompanyDto>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }

                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ApiResponse<ResCompanyDto>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                        ErrorMessage = "Compañía no encontrada."
                    };
                }

                var resCompanyDto = _mapper.Map<ResCompanyDto>(company);

                return new ApiResponse<ResCompanyDto>
                {
                    Success = true,
                    Data = resCompanyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResCompanyDto>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al obtener la compañía: {ex.Message}"
                };
            }
        }

        // UPDATE
        public async Task<ApiResponse<ResCompanyDto>> UpdateCompany(int id, ReqCompanyDto reqCompanyDto)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();

                if (responseJWT.Success == false)
                {
                    return new ApiResponse<ResCompanyDto>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }

                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ApiResponse<ResCompanyDto>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                        ErrorMessage = "Compañía no encontrada."
                    };
                }

                _mapper.Map(reqCompanyDto, company);
                company.StatusCompany = 1;

                _context.Companies.Update(company);
                await _context.SaveChangesAsync();

                var resCompanyDto = _mapper.Map<ResCompanyDto>(company);

                return new ApiResponse<ResCompanyDto>
                {
                    Success = true,
                    Data = resCompanyDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResCompanyDto>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al actualizar la compañía: {ex.Message}"
                };
            }
        }

        // DELETE (Cambiar estado en lugar de eliminar)
        public async Task<ApiResponse<string>> DeleteCompany(int id)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();

                if (responseJWT.Success == false)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }

                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                        ErrorMessage = "Compañía no encontrada."
                    };
                }

                company.StatusCompany = 0; // Cambiamos el estado a 0 para inactivar la compañía
                _context.Companies.Update(company);
                await _context.SaveChangesAsync();

                return new ApiResponse<string>
                {
                    Success = true,
                    Data = "Compañía eliminada exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al eliminar la compañía: {ex.Message}"
                };
            }
        }

        // LIST ALL
        public async Task<ApiResponse<List<ResCompanyDto>>> ListCompanies()
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                    {
                    return new ApiResponse<List<ResCompanyDto>>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }

                var companies = await _context.Companies
                    .Where(c => c.StatusCompany == 1)
                    .ToListAsync();

                var resCompanyDtos = _mapper.Map<List<ResCompanyDto>>(companies);

                return new ApiResponse<List<ResCompanyDto>>
                {
                    Success = true,
                    Data = resCompanyDtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResCompanyDto>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Error al listar las compañías: {ex.Message}"
                };
            }
        }
    }
}

