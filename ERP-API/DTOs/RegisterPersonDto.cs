using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class RegisterPersonDto
    {


            [StringLength(100)]
            public string? Name { get; set; }
            [StringLength(100)]
            public string? LastName { get; set; }
            [StringLength(100)]     
            public string? SecondLastName { get; set; }
            public double? Age { get; set; }
            [StringLength(100)]
            public string? PhoneNumber { get; set; }
            [StringLength(100)]
            public string? Address { get; set; }
            [StringLength(100)]
            public string? Nationality { get; set; }
            [StringLength(100)]
            public string? Identification { get; set; }
            [StringLength(100)]
            public string? CompanyCode { get; set; }
        

    }
}
