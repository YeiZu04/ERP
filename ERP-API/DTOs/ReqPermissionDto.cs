using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
        public class ReqPermissionDto
        {
            [Required]
            [StringLength(100)]
            public string? NamePermission { get; set; }

            [StringLength(250)]
            public string? DescriptionPermission { get; set; }
        }
    
}
