using ERP_API.DTOs;
using ERP_API.Models;
using Microsoft.EntityFrameworkCore;
using ERP_API.Services.Tools;
using Newtonsoft.Json.Linq;
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



        public EmployeeService(ERPDbContext context, PasswordHash passwordHash, RandomGenerator randomGenerator, SendEmail sendEmail, BearerCode bearerCode, IMapper mapper )
        {
            _context = context;
            _passwordHash = passwordHash;
            _randomGenerator = randomGenerator;
            _emailSend = sendEmail;
            _bearerCode = bearerCode;
            _Mapper = mapper;
            
        }
        
        public async Task<Api_Response.ApiResponse <string>> RegisterEmployeeAsync(ReqEmployeeDto ReqEmployeeDto)
        {



            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var responseJWT = await _bearerCode.VerficationCode();
                    if (responseJWT.Success == false)
                    {
                        return new Api_Response.ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = responseJWT.ErrorCode,
                            ErrorMessage = responseJWT.ErrorMessage
                        };
                    }

                    var Company = responseJWT.Data.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;

                    Console.WriteLine(Company);
                    if ( await UserExistsByUserName(ReqEmployeeDto.UserDto.UserName, Company.IdCompany))
                    {
                        return new Api_Response.ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = Api_Response.ErrorCode.UserAlreadyExists,
                            ErrorMessage = "El userName ya esta registrado"
                        };
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

                    // Verificar si el correo (usuario) ya existe en la empresa 
                    
                    if (await UserExistsByEmail(ReqEmployeeDto?.PersonDto?.EmailPerson, Company.IdCompany))
                    {
                        return new Api_Response.ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = Api_Response.ErrorCode.UserAlreadyExists,
                            ErrorMessage = "Este correo electronico ya existe en esta empresa."
                        };
                    }

                    // 1. Registrar la Persona
                    var person = _Mapper.Map<Person>(ReqEmployeeDto?.PersonDto);
                    person.IdCompanyFk = Company.IdCompany;
                    person.PersonUUID =  Guid.NewGuid();
                    person.StatePerson = 1;

                    _context.Person.Add(person);
                    await _context.SaveChangesAsync();

                    // Obtener el ID de la persona recién insertada
                    var personId = person.IdPerson;

                    // 2. Registrar el Usuario
                    var ramdomPaswword = _randomGenerator.GenerateRandomPassword();
                    var newPassword = _passwordHash.HashPassword(ramdomPaswword);

                    var user = new User
                    {
                        NameUser = ReqEmployeeDto.UserDto.UserName,
                        CreationDateUser = ReqEmployeeDto.UserDto.CreationDateUser,
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
                        IdRoleFk = ReqEmployeeDto?.UserRoleDto?.IdRole
                    };

                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();

                    // 4. Registrar el Empleado
                    var employee = _Mapper.Map<Employee>(ReqEmployeeDto?.EmployeeDto);
                    employee.IdUserFk =userId;
                    employee.VacationsEmployee = 0;

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    // Obtener el ID del empleado recién insertado
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

                    // Devolver el éxito con el ID del empleado registrado y envia el correo con tu contraseña.
                    await _emailSend.SendEmailAsync(ReqEmployeeDto.PersonDto.EmailPerson , "Bienvenido a la empresa", "Tu password será:  " + ramdomPaswword +" /n Te recomendamos cambiarla");

                    // 6. Confirmar la transacción
                    await transaction.CommitAsync();

                    
                    return new Api_Response.ApiResponse<string>
                    {

                        Success = true,
                        Data = "registro exitoso"
                    };
                }
                catch (Exception ex)
                {
                    // 7. Si hay un error, revertir los cambios
                    await transaction.RollbackAsync();

                    // Manejar el error y devolver un ApiResponse con el código y mensaje de error
                    return new Api_Response.ApiResponse<string>
                    {
                        Success = false,
                        ErrorCode = Api_Response.ErrorCode.GeneralError,
                        ErrorMessage = $"Ocurrió un error durante el registro: {ex.Message}"
                    };
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


            // Verificar si el nombre de usuario ya existe en la compañía
            return await _context.Users
                .AnyAsync(u => u.NameUser == userName && u.IdPersonFkNavigation.IdCompanyFk == idCompany);
        }


    }
}
