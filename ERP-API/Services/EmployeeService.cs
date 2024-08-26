using ERP_API.DTOs;
using ERP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_API.Services
{
    public class EmployeeService
    {
        private readonly ERPDbContext _context;

        public EmployeeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task RegisterEmployeeAsync(
     RegisterPersonDto personDto,
     RegisterUserDto userDto,
     RegisterEmployeeDto employeeDto,
     RegisterUserRoleDto userRoleDto,
     RegisterCurriculumDto curriculumDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Obtener el ID de la compañía usando el código
                    var companyId = await GetCompanyIdByCodeAsync(personDto.CompanyCode);

                    if (companyId == null)
                    {
                        throw new Exception("La compañía con el código proporcionado no existe.");
                    }

                    // 1. Registrar la Persona
                    var person = new Person
                    {
                        NamePerson = personDto.Name,
                        LastNamePerson = personDto.LastName,
                        AgePerson = personDto.Age,
                        PhoneNumberPerson = personDto.PhoneNumber,
                        AddressPerson = personDto.Address,
                        NationalityPerson = personDto.Nationality,
                        IdentificationPerson = personDto.Identification,
                        IdCompanyFk = companyId.Value // Asignar la FK de la compañía
                    };

                    _context.Person.Add(person);
                    await _context.SaveChangesAsync();

                    // 2. Registrar el Usuario
                    var user = new User
                    {
                        UserName = userDto.UserName,
                        PasswordUser = userDto.Password,
                        IdPersonFk = person.IdPerson // Asignar la FK al usuario
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    // 3. Asignar el Rol al Usuario
                    var userRole = new UserRole
                    {
                        IdUserFk = user.IdUser,
                        IdRoleFk = userRoleDto.IdRole
                    };

                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();

                    // 4. Registrar el Empleado
                    var employee = new Employee
                    {
                        DepartmentEmployee = employeeDto.DepartmentEmployee,
                        HiringDateEmployee = employeeDto.HiringDateEmployee,
                        NetSalaryEmployee = employeeDto.NetSalaryEmployee,
                        PositionEmployee = employeeDto.PositionEmployee,
                        IdUserFk = user.IdUser // Asignar la FK al usuario
                    };

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    // 5. Registrar el Currículum
                    var curriculum = new Curriculum
                    {
                        IdEmployeeFk = employee.IdEmployee,
                        CurriculumPath = curriculumDto.CurriculumPath,
                        UpdateDate = curriculumDto.UpdateDate
                    };

                    _context.Curriculums.Add(curriculum);
                    await _context.SaveChangesAsync();

                    // 6. Confirmar la transacción
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // 7. Si hay un error, revertir los cambios
                    await transaction.RollbackAsync();
                    throw;
                }
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
