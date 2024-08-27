using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class RoleDto
    {
        [StringLength(100)]
        public string? TypeRole { get; set; }

    }
}