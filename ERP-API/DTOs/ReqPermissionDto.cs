using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
        public class ReqPermissionDto
        {
            
            public int IdPermission { get; set; }

            public string? NamePermission { get; set; }

            public string? DescriptionPermission { get; set; }
        }
    
}
