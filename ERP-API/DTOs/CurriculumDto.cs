using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class CurriculumDto
    {
       
        [StringLength(100)]
        public string? PathFileCurriculum { get; set; }
        public DateTime DateUpload { get; set; }
    }
 } 
  