using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class LoginDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public DateTime Created_at { get; set; }
        [Required]
        public string?  CodeCompany { get; set; }

    }
}
