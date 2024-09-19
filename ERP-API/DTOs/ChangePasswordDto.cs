using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class ChangePasswordDto
    {

        public string? Password { get; set; }

        [Required]
        public string? NewPassword { get; set; }
    }
}
