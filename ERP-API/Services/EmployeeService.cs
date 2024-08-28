using ERP_API.DTOs;
using ERP_API.Models;
using Microsoft.EntityFrameworkCore;
using static ERP_API.Services.ResponseService;

namespace ERP_API.Services
{
    public class EmployeeService
    {
        private readonly ERPDbContext _context;

        private readonly PasswordHash _passwordHash;

        private readonly SendEmail _emailSend;

        private readonly RandomGenerator _randomGenerator;

        public EmployeeService(ERPDbContext context, PasswordHash passwordHash, RandomGenerator randomGenerator, SendEmail sendEmail)
        {
            _context = context;
            _passwordHash = passwordHash;
            _randomGenerator = randomGenerator;
            _emailSend = sendEmail;
        }

        public async Task<ApiResponse<string>> RegisterEmployeeAsync(RegisterEmployee employeeDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Verificar si el usuario ya existe por correo electrónico
                    if (await UserExistsByEmail(employeeDto.PersonDto.Email))
                    {
                        return new ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = ErrorCode.UserAlreadyExists,
                            ErrorMessage = "El usuario con este correo electrónico ya existe."
                        };
                    }

                    // Obtener el ID de la compañía usando el código
                    var companyId = await GetCompanyIdByCodeAsync(employeeDto.PersonDto.CompanyCode);

                    if (companyId == null)
                    {
                        return new ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = ErrorCode.InvalidInput,
                            ErrorMessage = "Código de compañía inválido."
                        };
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
                    var ramdomPaswword = _randomGenerator.GenerateRandomPassword();
                    var newPassword = _passwordHash.HashPassword(ramdomPaswword);

                    var user = new User
                    {
                        NameUser = employeeDto.UserDto.UserName,
                        CreationDateUser = employeeDto.UserDto.CreationDateUser,
                        PasswordUser = newPassword,
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

                    // Devolver el éxito con el ID del empleado registrado y envia el correo con tu contraseña.
                    await _emailSend.SendEmailAsync(employeeDto.PersonDto.Email, "Bienvenido a la empresa", "Tu password será:  " + ramdomPaswword);

                    // 6. Confirmar la transacción
                    await transaction.CommitAsync();

                    
                    return new ApiResponse<string>
                    {

                        Success = true,
                        Data = employeeId.ToString()
                    };
                }
                catch (Exception ex)
                {
                    // 7. Si hay un error, revertir los cambios
                    await transaction.RollbackAsync();

                    // Manejar el error y devolver un ApiResponse con el código y mensaje de error
                    return new ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.GeneralError,
                        ErrorMessage = $"Ocurrió un error durante el registro: {ex.Message}"
                    };
                }
            }
        }

        private async Task<int?> GetCompanyIdByCodeAsync(string companyCode)
        {
            var company = await _context.Companies
                                        .FirstOrDefaultAsync(c => c.CodeCompany == companyCode);
            return company?.IdCompany;
        }

        private async Task<bool> UserExistsByEmail(string email)
        {
            return await _context.Person.AnyAsync(u => u.EmailPerson == email);
        }
    }
}
