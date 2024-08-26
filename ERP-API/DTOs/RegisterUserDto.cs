using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class RegisterUserDto
    {
        [StringLength(100)]
        public string? UserName { get; set; }
        [StringLength(100)]
        public string? Password { get; set; } // Opcional, si se establece por defecto
    }
}
