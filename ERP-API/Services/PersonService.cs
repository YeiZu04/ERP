using ERP_API.Models;
using ERP_API.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_API.Services.Tools;

namespace ERP_API.Services
{
    public class PersonService
    {
        private readonly ERPDbContext _context;
        private readonly IMapper _mapper;
        private readonly BearerCode _bearerCode;

        public PersonService(ERPDbContext eRPDbContext, IMapper mapper, BearerCode bearerCode)
        {
            _context = eRPDbContext;
            _mapper = mapper;
            _bearerCode = bearerCode;
        }

        public async Task<ResPersonDto> UpdatePerson(ResPersonDto resPersonDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var company = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;

            var person = await _context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonUUID == resPersonDto.PersonUUID && p.IdCompanyFk == company.IdCompany);

            if (person == null)
            {
                throw new KeyNotFoundException("Persona no encontrada.");
            }

            var newPerson = _mapper.Map<Person>(resPersonDto);
            newPerson.IdPerson = person.IdPerson;
            newPerson.IdCompanyFk = company.IdCompany;
            newPerson.StatePerson = 1;

            _context.Person.Update(newPerson);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResPersonDto>(newPerson);
        }

        public async Task<List<ResPersonDto>> ListPerson()
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var companyId = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;

            var people = await _context.Person
                .Where(p => p.IdCompanyFk == companyId.IdCompany && p.StatePerson == 1)
                .ToListAsync();

            return _mapper.Map<List<ResPersonDto>>(people);
        }

        public async Task<string> DeletePerson(ResPersonDto resPersonDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (!responseJWT.Success)
            {
                throw new UnauthorizedAccessException(responseJWT.ErrorMessage);
            }

            var company = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;

            var person = await _context.Person
                .FirstOrDefaultAsync(p => p.PersonUUID == resPersonDto.PersonUUID && p.IdCompanyFk == company.IdCompany);

            if (person == null)
            {
                throw new KeyNotFoundException("Persona no encontrada.");
            }

            person.StatePerson = 0;
            _context.Person.Update(person);
            await _context.SaveChangesAsync();

            return "Persona eliminada correctamente.";
        }

        private async Task<int?> GetCompanyIdByCodeAsync(string companyCode)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.CodeCompany == companyCode);
            return company?.IdCompany;
        }
    }
}
