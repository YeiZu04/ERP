using ERP_API.Models;
using ERP_API.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ERP_API.Services.Tools.Api_Response;
using System.ComponentModel.Design;
using ERP_API.Services.Tools;
using ERP_API.Interfaces;
namespace ERP_API.Services

{
    public class PersonService : IPersonService
    {

        private readonly ERPDbContext _context;

        private readonly IMapper _mapper;

        private readonly BearerCode _bearerCode;
        public PersonService( ERPDbContext eRPDbContext, IMapper mapper, BearerCode bearerCode) { 

            _context = eRPDbContext;
            _mapper = mapper;
            _bearerCode = bearerCode;
        }


        public async Task<ApiResponse<ResPersonDto>> UpdatePerson(ResPersonDto resPersonDto)
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new ApiResponse<ResPersonDto>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }

                var Company = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;
                // Buscar la persona existente en la base de datos
                var person = await _context.Person.AsNoTracking().FirstOrDefaultAsync(p => p.PersonUUID == resPersonDto.PersonUUID && p.IdCompanyFk == Company.IdCompany);

                // Verificar si la persona existe
                if (person == null)
                {
                    return new ApiResponse<ResPersonDto>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                        ErrorMessage = "Persona no encontrada."
                    };
                }

                // Mapear los datos del DTO al objeto Person
                var newPerson= _mapper.Map<Person>(resPersonDto);
                newPerson.IdPerson = person.IdPerson;
                newPerson.IdCompanyFk = Company?.IdCompany;
                newPerson.StatePerson = 1;

                _context.Person.Update(newPerson);
                await _context.SaveChangesAsync();

                // Retornar respuesta exitosa
                return new ApiResponse<ResPersonDto>
                {
                    Success = true,
                    Data = _mapper.Map<ResPersonDto>(newPerson)
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return new ApiResponse<ResPersonDto>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Ocurrió un error durante la actualización: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List<ResPersonDto>>> ListPerson()
        {
            try
            {
                var responseJWT = await _bearerCode.VerficationCode();
                if (responseJWT.Success == false)
                {
                    return new ApiResponse<List<ResPersonDto>>
                    {
                        Success = false,
                        ErrorCode = responseJWT.ErrorCode,
                        ErrorMessage = responseJWT.ErrorMessage
                    };
                }

                var companyId = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;

                // Obtén la lista de personas desde la base de datos
                var people = await _context.Person
                    .Where(p => p.IdCompanyFk == companyId.IdCompany && p.StatePerson == 1) 
                    .ToListAsync();

                // Mapea la lista de entidades Person a PersonDto
                var ListPerson = _mapper.Map<List<ResPersonDto>>(people);

                // Retorna una respuesta con éxito y la lista de PersonDto
                return new ApiResponse<List<ResPersonDto>>
                {
                    Success = true,
                    Data = ListPerson
                };
            }
            catch (Exception ex)
            {
                // Retorna una respuesta de error en caso de excepción
                return new ApiResponse<List<ResPersonDto>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Ocurrió un error durante la obtención de la lista de personas: {ex.Message}"
                };
            }
        }


        public async Task<ApiResponse<string>> DeletePerson(ResPersonDto resPersonDto)
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

                var Company = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;
               
                // Obtén la lista de personas desde la base de datos
                var person = await _context.Person.FirstOrDefaultAsync(p => p.PersonUUID == resPersonDto.PersonUUID && p.IdCompanyFk == Company.IdCompany);

                // se coloca el estado de la persona en 0 para que no este activa.
                person.StatePerson = 0;

                _context.Person.Update(person);
                await _context.SaveChangesAsync();
                // Retorna una respuesta con éxito y la lista de PersonDto
                return new ApiResponse<string>
                {
                    Success = true,
                    Data = "Se elimino la persona "
                };
            }
            catch (Exception ex)
            {
                // Retorna una respuesta de error en caso de excepción
                return new ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = ErrorCode.GeneralError,
                    ErrorMessage = $"Ocurrió un error durante la obtención de la lista de personas: {ex.Message}"
                };
            }
        }

        private async Task<int?> GetCompanyIdByCodeAsync(string companyCode)
        {
            var company = await _context.Companies
                                        .FirstOrDefaultAsync(c => c.CodeCompany == companyCode);
            return company?.IdCompany;
        }


    }
}
