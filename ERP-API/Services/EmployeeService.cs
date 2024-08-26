using ERP_API.DTOs;
using ERP_API.Models;

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

                // con esto personDto.CompanyCode vamos a buscar el id de la compa;ia en la tabla company  donde el campo en el que tenemos que buscar es IdCompany
                try
                {
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
                        IdCompanyFkNavigation = personDto.CompanyCode
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

    }
}
