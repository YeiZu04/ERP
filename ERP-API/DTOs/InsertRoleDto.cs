using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class InsertRoleDto
    {
        [StringLength(100)]
        public string? TypeRole { get; set; }

    }
}