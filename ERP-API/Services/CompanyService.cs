using ERP_API.DTOs;
using ERP_API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_API.Services.Tools;
using ERP_API.Interfaces;


namespace ERP_API.Services
{
    public class CompanyService : ICompanyService
    {
             private readonly ERPDbContext _context;
        private readonly IMapper _mapper;
        private readonly BearerCode _bearerCode;

        public CompanyService(ERPDbContext context, IMapper mapper, BearerCode bearerCode)
        {
            _context = context;
            _mapper = mapper;
            _bearerCode = bearerCode;
        }

        // CREATE
        public async Task<ResCompanyDto> CreateCompany(ReqCompanyDto reqCompanyDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (responseJWT == null)
                throw new UnauthorizedAccessException("Acceso no autorizado.");

            var company = _mapper.Map<Company>(reqCompanyDto);
            company.StatusCompany = 1;

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResCompanyDto>(company);
        }

        // UPDATE
        public async Task<ResCompanyDto> UpdateCompany(ReqCompanyDto reqCompanyDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if ( responseJWT == null)
                throw new UnauthorizedAccessException("Acceso no autorizado.");

            var company = await _context.Companies.FindAsync(reqCompanyDto.IdCompany);
            if (company == null)
                throw new KeyNotFoundException("Compañía no encontrada.");

            _mapper.Map(reqCompanyDto, company);
            company.StatusCompany = 1;

            _context.Companies.Update(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<ResCompanyDto>(company);
        }

        // DELETE (Cambiar estado en lugar de eliminar)
        public async Task DeleteCompany(ReqCompanyDto reqCompanyDto)
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (responseJWT == null)
                throw new UnauthorizedAccessException("Acceso no autorizado.");

            var company = await _context.Companies.FindAsync(reqCompanyDto.IdCompany);
            if (company == null)
                throw new KeyNotFoundException("Compañía no encontrada.");

            company.StatusCompany = 0; // Cambiamos el estado a inactivo
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        // LIST ALL
        public async Task<List<ResCompanyDto>> ListCompanies()
        {
            var responseJWT = await _bearerCode.VerficationCode();
            if (responseJWT == null)
                throw new UnauthorizedAccessException("Acceso no autorizado.");

            var companies = await _context.Companies
                .Where(c => c.StatusCompany == 1)
                .ToListAsync();

            return _mapper.Map<List<ResCompanyDto>>(companies);
        }
    }
}

