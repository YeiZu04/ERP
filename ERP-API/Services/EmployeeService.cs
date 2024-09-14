using ERP_API.DTOs;
using ERP_API.Models;
using Microsoft.EntityFrameworkCore;
using ERP_API.Services.Tools;
using Newtonsoft.Json.Linq;

namespace ERP_API.Services
{
    public class EmployeeService
    {
        private readonly ERPDbContext _context;

        private readonly PasswordHash _passwordHash;

        private readonly SendEmail _emailSend;

        private readonly RandomGenerator _randomGenerator;

        private readonly BearerCode _bearerCode;



        public EmployeeService(ERPDbContext context, PasswordHash passwordHash, RandomGenerator randomGenerator, SendEmail sendEmail, BearerCode bearerCode )
        {
            _context = context;
            _passwordHash = passwordHash;
            _randomGenerator = randomGenerator;
            _emailSend = sendEmail;
            _bearerCode = bearerCode;
            
        }
        
        public async Task<Api_Response.ApiResponse <string>> RegisterEmployeeAsync(RegisterEmployee employeeDto)
        {

            var responseJWT = await _bearerCode.VerficationCode(employeeDto.JwtToken);
            if (responseJWT.Success == false)
            {
                return new Api_Response.ApiResponse<string>
                {
                    Success = false,
                    ErrorCode = responseJWT.ErrorCode,
                    ErrorMessage = responseJWT.ErrorMessage
                };
            }

            var session = await _context.Sessions
              .Include(s => s.IdUserFkNavigation) // Relación con User
                  .ThenInclude(u => u.IdPersonFkNavigation) // Relación con Person
                  .ThenInclude(p => p.IdCompanyFkNavigation) // Relación con Company
              .FirstOrDefaultAsync(s => s.TokenSession == employeeDto.JwtToken); // Filtrar por el token JWT

            var idCompany = session?.IdUserFkNavigation?.IdPersonFkNavigation?.IdCompanyFkNavigation;
            if (idCompany == null)
            {
               
            }
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                   
                    // Obtener el ID de la compañía usando el código
                    var companyId = await GetCompanyIdByCodeAsync(employeeDto.PersonDto.CompanyCode);

                    if (companyId == null)
                    {
                        return new Api_Response.ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = Api_Response.ErrorCode.InvalidInput,
                            ErrorMessage = "Código de compañía inválido."
                        };
                    }
                    /*
                    // verificar si el correo propocionado existe en algun SmtpClient
                    if ( await _emailSend.VerifyEmailExists(employeeDto.PersonDto.Email))
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
                    if (await UserExistsByEmail(employeeDto.PersonDto.Email, companyId.Value))
                    {
                        return new Api_Response.ApiResponse<string>
                        {
                            Success = false,
                            ErrorCode = Api_Response.ErrorCode.UserAlreadyExists,
                            ErrorMessage = "Este correo electronico ya existe en esta empresa."
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
                        PersonUUID = Guid.NewGuid(),
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
                    await _emailSend.SendEmailAsync(employeeDto.PersonDto.Email, "Bienvenido a la empresa", "Tu password será:  " + ramdomPaswword +" /n Te recomendamos cambiarla");

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

        private async Task<int?> GetCompanyIdByCodeAsync(string companyCode)
        {
            var company = await _context.Companies
                                        .FirstOrDefaultAsync(c => c.CodeCompany == companyCode);
            return company?.IdCompany;
        }

        private async Task<bool> UserExistsByEmail(string email, int companyId)
        {
            return await _context.Person
                .AnyAsync(u => u.EmailPerson == email && u.IdCompanyFk == companyId);
        }

    }
}
