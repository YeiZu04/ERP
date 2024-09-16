using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class EmployeeDto
    {

        public DateTime HiringDateEmployee { get; set; }
        public double NetSalaryEmployee { get; set; }
        public string? PositionEmployee { get; set; }       
        public string? DepartmentEmployee { get; set; }
        

    }
}
