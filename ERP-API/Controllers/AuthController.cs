using ERP_API.DTOs;
using ERP_API.Services;
using ERP_API.Services.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ERP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginService _authService;

        public AuthController(LoginService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
        {
           
                var response = await _authService.Authenticate(loginDTO);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return response.ErrorCode switch
                {
                    Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                    _ => StatusCode(500, new { message = response.ErrorMessage })
                };
            }
               
            
           
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.Logout();

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return response.ErrorCode switch
                {
                    Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                    _ => StatusCode(500, new { message = response.ErrorMessage })
                };

            }

        }   
           

        [HttpPost("RecoveryPassword")]
            public async Task<IActionResult> RecoveryPassword([FromBody] RecoveryPasswordDto recoveryPasswordDto)
        {
            // Llama al servicio para registrar al empleado
            var result = await _authService.RecoveryPassword(recoveryPasswordDto);

            // Verifica si la operación fue exitosa
            if (result.Success)
            {
                // Devuelve una respuesta 200 OK con el ID del empleado registrado
                return Ok(result);
            }
            else
            {
                // Devuelve una respuesta con el código de error adecuado
                return result.ErrorCode switch
                {
                    Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = result.ErrorMessage }),
                    Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = result.ErrorMessage }),
                    Api_Response.ErrorCode.NotFound => NotFound(new { message = result.ErrorMessage }),
                    Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = result.ErrorMessage }),
                    _ => StatusCode(500, new { message = result.ErrorMessage })
                };
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePassword)
        {
            var response = await _authService.ChangePassword(changePassword);
            if (response.Success)
            {

                return Ok(response);
            }
            else
            {
              
                return response.ErrorCode switch
                {
                    Api_Response.ErrorCode.UserAlreadyExists => Conflict(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.InvalidInput => BadRequest(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.NotFound => NotFound(new { message = response.ErrorMessage }),
                    Api_Response.ErrorCode.errorDataBase => StatusCode(500, new { message = response.ErrorMessage }),
                    _ => StatusCode(500, new { message = response.ErrorMessage })
                };
            }
        }

    }
}
