using System.ComponentModel.DataAnnotations;

namespace ERP_API.DTOs
{
    public class InsertBenefitDto

    {
        [StringLength(100)]
        public string? BenefitName { get; set; }
        [StringLength(200)]
        public string? BenefitDescription { get; set; }
    }
}
