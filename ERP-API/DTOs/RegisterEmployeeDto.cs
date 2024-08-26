using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class RegisterEmployeeDto
    {

        public DateTime HiringDate { get; set; }
        public double NetSalary { get; set; }
        [StringLength(100)]
        public string? Position { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }
        public int UserId { get; set; }// opcional depende de donde se obtenga el id del nuevo User registrado.

    }
}
