using ERP_API.DTOs;
using ERP_API.Services;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var token = await _authService.Authenticate(loginDTO);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.Logout();
                return Ok("Sesión cerrada exitosamente.");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
