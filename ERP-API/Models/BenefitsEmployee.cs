using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class BenefitsEmployee
{
    public int IdBenefitsEmployee { get; set; }

    public int IdEmployeeFk { get; set; }

    public int IdBenefitsFk { get; set; }

    public virtual Benefit IdBenefitsFkNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeFkNavigation { get; set; } = null!;
}
