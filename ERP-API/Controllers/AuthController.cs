using ERP_API.DTOs;
using ERP_API.Services;
using Microsoft.AspNetCore.Mvc;

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
            var token = await _authService.Authenticate(loginDTO);
            return Ok(new { token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.Logout();
            return Ok(new { message = result });
        }

        [HttpPost("RecoveryPassword")]
        public async Task<IActionResult> RecoveryPassword([FromBody] RecoveryPasswordDto recoveryPasswordDto)
        {
            var result = await _authService.RecoveryPassword(recoveryPasswordDto);
            return Ok(new { message = result });
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePassword)
        {
            var result = await _authService.ChangePassword(changePassword);
            return Ok(new { message = result });
        }
    }
}
