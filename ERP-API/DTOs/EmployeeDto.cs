using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class EmployeeDto
    {

        public DateTime HiringDate { get; set; }
        public double NetSalary { get; set; }
        [StringLength(100)]
        public string? Position { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }
        

    }
}
