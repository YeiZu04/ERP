using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class PersonDto
    {


            [StringLength(100)]
            public string? NamePerson { get; set; }
            [StringLength(100)]
            public string? LastNamePerson { get; set; }
            [StringLength(100)]     
            public string? SecondLastNamePerson { get; set; }
            public double? AgePerson { get; set; }
            [StringLength(100)]
            public string? PhoneNumberPerson { get; set; }
            [StringLength(100)]
            public string? AddressPerson { get; set; }
            [StringLength(100)]
            public string? NationalityPerson { get; set; }
            [StringLength(100)]
            public string? IdentificationPerson { get; set; }
            [StringLength(100)]
            public string? EmailPerson { get; set; }

            [StringLength(100)]
            public string? CompanyCode { get; set; }

            public Guid UUID { get; set; }


    }
}
