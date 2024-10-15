using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Warning
{
    public int IdWarning { get; set; }

    public string ReasonWarning { get; set; } = null!;

    public string? DescrptionWarning { get; set; }

    public int IdEmployeedFk { get; set; }

    public DateTime DateWarning { get; set; }

    public int? IdCompanyFk { get; set; }

    public virtual Company? IdCompanyFkNavigation { get; set; }

    public virtual Employee IdEmployeedFkNavigation { get; set; } = null!;
}
