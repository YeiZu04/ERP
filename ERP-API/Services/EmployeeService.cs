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

        public async Task RegisterEmployeeAsync(RegisterEmployee employeeDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Obtener el ID de la compañía usando el código
                    var companyId = await GetCompanyIdByCodeAsync(employeeDto.PersonDto.CompanyCode);

                    if (companyId == null)
                    {
                        throw new Exception("La compañía con el código proporcionado no existe.");
                    }

                    // 1. Registrar la Persona
                    var person = new Person
                    {
                        NamePerson = employeeDto.PersonDto.Name,
                        LastNamePerson = employeeDto.PersonDto.LastName,
                        SecondLastNamePerson = employeeDto.PersonDto.SecondLastName,
                        AgePerson = employeeDto.PersonDto.Age,
                        PhoneNumberPerson = employeeDto.PersonDto.PhoneNumber,
                        AddressPerson = employeeDto.PersonDto.Address,
                        NationalityPerson = employeeDto.PersonDto.Nationality,
                        StatePerson = 1,
                        IdentificationPerson = employeeDto.PersonDto.Identification,
                        EmailPerson = employeeDto.PersonDto.Email,
                        IdCompanyFk = companyId.Value // Asignar la FK de la compañía
                    };

                    _context.Person.Add(person);
                    await _context.SaveChangesAsync();

                    // Obtener el ID de la persona recién insertada
                    var personId = person.IdPerson;

                    // 2. Registrar el Usuario
                    var user = new User
                    {
                        UserName = employeeDto.UserDto.UserName,
                        CreationDateUser = employeeDto.UserDto.CreationDateUser,
                        PasswordUser = employeeDto.UserDto.Password,
                        IdPersonFk = personId // Asignar la FK al usuario
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    // Obtener el ID del usuario recién insertado
                    var userId = user.IdUser;

                    // 3. Asignar el Rol al Usuario
                    var userRole = new UserRole
                    {
                        IdUserFk = userId, // Asignar la FK del usuario
                        IdRoleFk = employeeDto.UserRoleDto.IdRole
                    };

                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();

                    // 4. Registrar el Empleado
                    var employee = new Employee
                    {
                        DepartmentEmployee = employeeDto.EmployeeDto.Department,
                        HiringDateEmployee = employeeDto.EmployeeDto.HiringDate,
                        NetSalaryEmployee = employeeDto.EmployeeDto.NetSalary,
                        PositionEmployee = employeeDto.EmployeeDto.Position,
                        VacationsEmployee = 0,
                        IdUserFk = userId // Asignar la FK al usuario
                    };

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    // Obtener el ID del empleado recién insertado
                    var employeeId = employee.IdEmployee;

                    // 5. Registrar el Currículum
                    var curriculum = new Curriculum
                    {
                        IdEmployeeFk = employeeId, // Asignar la FK del empleado
                        PathFileCurriculum = employeeDto.CurriculumDto.PathFileCurriculum,
                        DateUploaded = employeeDto.CurriculumDto.DateUpload
                    };

                    _context.Curriculum.Add(curriculum);
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
