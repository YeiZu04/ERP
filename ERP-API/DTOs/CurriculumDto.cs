using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class CurriculumDto
    {
        public int IdEmployeed { get; set; }
        [StringLength(100)]
        public string? PathFileCurriculum { get; set; }
        public DateTime DateUpload { get; set; }
    }
 } 
  