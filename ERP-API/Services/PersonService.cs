using ERP_API.Models;
using ERP_API.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ERP_API.Services.Tools.Api_Response;
using System.ComponentModel.Design;
using ERP_API.Services.Tools;
namespace ERP_API.Services

{
    public class PersonService
    {

        private readonly ERPDbContext _context;

        private readonly IMapper _mapper;
        public PersonService( ERPDbContext eRPDbContext, IMapper mapper) { 

            _context = eRPDbContext;
            _mapper = mapper;
        
        }


        public async Task<Api_Response.ApiResponse<string>> UpdatePerson(PersonDto personDto)
        {
            try
            {
                var companyId = await GetCompanyIdByCodeAsync(personDto.CompanyCode);
                // Buscar la persona existente en la base de datos
                var person = await _context.Person.SingleOrDefaultAsync(p => p.PersonUUID == personDto.UUID && p.IdCompanyFk == companyId);

                // Verificar si la persona existe
                if (person == null)
                {
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.NotFound,
                        ErrorMessage = "Persona no encontrada."
                    };
                }

                // Mapear los datos del DTO al objeto Person
                _mapper.Map(personDto, person);

                _context.Person.Update(person);
                await _context.SaveChangesAsync();

                // Retornar respuesta exitosa
                return new Api_Response.ApiResponse<string>
                {
                    Success = true,
                    Data = "Actualización de persona exitosa"
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
                    ErrorMessage = $"Ocurrió un error durante la actualización: {ex.Message}"
                };
            }
        }

        public async Task<Api_Response.ApiResponse<List<PersonDto>>> ListPerson(int idCompany)
        {
            try
            {
                // Obtén la lista de personas desde la base de datos
                var people = await _context.Person
                    .Where(p => p.IdCompanyFk == idCompany && p.StatePerson == 1) 
                    .ToListAsync();

                // Mapea la lista de entidades Person a PersonDto
                var ListPerson = _mapper.Map<List<PersonDto>>(people);

                // Retorna una respuesta con éxito y la lista de PersonDto
                return new Api_Response.ApiResponse<List<PersonDto>>
                {
                    Success = true,
                    Data = ListPerson
                };
            }
            catch (Exception ex)
            {
                // Retorna una respuesta de error en caso de excepción
                return new Api_Response.ApiResponse<List<PersonDto>>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
                    ErrorMessage = $"Ocurrió un error durante la obtención de la lista de personas: {ex.Message}"
                };
            }
        }


        public async Task<Api_Response.ApiResponse<string>> DeletePerson(PersonDto personDto)
        {
            try
            {
                var companyId = await GetCompanyIdByCodeAsync(personDto.CompanyCode);
                // Obtén la lista de personas desde la base de datos
                var person = await _context.Person.SingleOrDefaultAsync(p => p.PersonUUID == personDto.UUID && p.IdCompanyFk == companyId);

                // Mapear los datos del DTO al objeto Person
                _mapper.Map(personDto, person);

                _context.Person.Update(person);
                await _context.SaveChangesAsync();
                // Retorna una respuesta con éxito y la lista de PersonDto
                return new Api_Response.ApiResponse<string>
                {
                    Success = true,
                    Data = "Se elimino la persona "
                };
            }
            catch (Exception ex)
            {
                // Retorna una respuesta de error en caso de excepción
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = Api_Response.ErrorCode.GeneralError,
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
