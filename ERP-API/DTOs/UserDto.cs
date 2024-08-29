using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class UserDto
    {
        [StringLength(100)]
        public string? UserName { get; set; }
        public DateTime? CreationDateUser { get; set; }
    }
}
