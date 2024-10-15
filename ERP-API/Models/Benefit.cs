using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Benefit
{
    public int IdBenefits { get; set; }

    public string NameBenefits { get; set; } = null!;

    public string? DescriptionBenefits { get; set; }

    public int? IdCompanyFk { get; set; }

    public virtual ICollection<BenefitsEmployee> BenefitsEmployees { get; set; } = new List<BenefitsEmployee>();

    public virtual Company? IdCompanyFkNavigation { get; set; }
}
