using ERP_API.DTOs;
using ERP_API.Models;
using Microsoft.EntityFrameworkCore;
using ERP_API.Services.Tools;
using AutoMapper;

namespace ERP_API.Services
{
    public class EmployeeService
    {
        private readonly ERPDbContext _context;
        private readonly PasswordHash _passwordHash;
        private readonly SendEmail _emailSend;
        private readonly RandomGenerator _randomGenerator;
        private readonly BearerCode _bearerCode;
        private readonly IMapper _Mapper;

        public EmployeeService(ERPDbContext context, PasswordHash passwordHash, RandomGenerator randomGenerator, SendEmail sendEmail, BearerCode bearerCode, IMapper mapper)
        {
            _context = context;
            _passwordHash = passwordHash;
            _randomGenerator = randomGenerator;
            _emailSend = sendEmail;
            _bearerCode = bearerCode;
            _Mapper = mapper;
        }

        public async Task<string> RegisterEmployeeAsync(ReqEmployeeDto ReqEmployeeDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var responseJWT = await _bearerCode.VerficationCode();
                    if (!responseJWT.Success)
                    {
                        throw new UnauthorizedAccessException("Acceso no autorizado.");
                    }

                    var Company = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;

                    if (await UserExistsByUserName(ReqEmployeeDto.UserDto.UserName, Company.IdCompany))
                    {
                        throw new InvalidOperationException("El userName ya está registrado.");
                    }

                    if (await UserExistsByEmail(ReqEmployeeDto?.PersonDto?.EmailPerson, Company.IdCompany))
                    {
                        throw new InvalidOperationException("Este correo electrónico ya existe en esta empresa.");
                    }

                    /*
                    // verificar si el correo propocionado existe en algun SmtpClient
                    if ( await _emailSend.VerifyEmailExists(ReqEmployeeDto.PersonDto.Email))
                    {
                        return new ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = ErrorCode.NotFound,
                            ErrorMessage = "El correo no existe ingrese uno valido."
                        };
                    }
                    */

                    // 1. Registrar la Persona
                    var person = _Mapper.Map<Person>(ReqEmployeeDto?.PersonDto);
                    person.IdCompanyFk = Company.IdCompany;
                    person.PersonUUID = Guid.NewGuid();
                    person.StatePerson = 1;

                    _context.Person.Add(person);
                    await _context.SaveChangesAsync();

                    var personId = person.IdPerson;

                    // 2. Registrar el Usuario
                    var ramdomPassword = _randomGenerator.GenerateRandomPassword();
                    var newPassword = _passwordHash.HashPassword(ramdomPassword);

                    var user = new User
                    {
                        NameUser = ReqEmployeeDto.UserDto.UserName,
                        CreationDateUser = ReqEmployeeDto.UserDto.CreationDateUser,
                        PasswordUser = newPassword,
                        IdPersonFk = personId // Asignar la FK al usuario
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    var userId = user.IdUser;

                    // 3. Asignar el Rol al Usuario
                    var userRole = new UserRole
                    {
                        IdUserFk = userId, // Asignar la FK del usuario
                        IdRoleFk = ReqEmployeeDto?.UserRoleDto?.IdRole
                    };

                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();

                    // 4. Registrar el Empleado
                    var employee = _Mapper.Map<Employee>(ReqEmployeeDto?.EmployeeDto);
                    employee.IdUserFk = userId;
                    employee.VacationsEmployee = 0;

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    var employeeId = employee.IdEmployee;

                    // 5. Registrar el Currículum
                    var curriculum = new Curriculum
                    {
                        IdEmployeeFk = employeeId, // Asignar la FK del empleado
                        PathFileCurriculum = ReqEmployeeDto?.CurriculumDto?.PathFileCurriculum,
                        DateUploaded = ReqEmployeeDto?.CurriculumDto?.DateUpload
                    };

                    _context.Curriculum.Add(curriculum);
                    await _context.SaveChangesAsync();

                    // 6. Enviar el correo con la contraseña generada
                    await _emailSend.SendEmailAsync(ReqEmployeeDto.PersonDto.EmailPerson, "Bienvenido a la empresa", "Tu password es: " + ramdomPassword + " Te recomendamos cambiarla.");

                    // 7. Confirmar la transacción
                    await transaction.CommitAsync();

                    return "Registro exitoso";
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;  // El middleware manejará la excepción
                }
            }
        }

        private async Task<bool> UserExistsByEmail(string email, int companyId)
        {
            return await _context.Person
                .AnyAsync(u => u.EmailPerson == email && u.IdCompanyFk == companyId);
        }

        private async Task<bool> UserExistsByUserName(string userName, int idCompany)
        {
            return await _context.Users
                .AnyAsync(u => u.NameUser == userName && u.IdPersonFkNavigation.IdCompanyFk == idCompany);
        }
    }
}
