using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class WorkSchedule
{
    public int IdWorkSchedule { get; set; }

    public string NameWorkSchedule { get; set; } = null!;

    public DateTime StartWorkSchedule { get; set; }

    public DateTime EndWorkSchedule { get; set; }

    public int IdEmployeerFk { get; set; }

    public int? IdCompanyFk { get; set; }

    public virtual Company? IdCompanyFkNavigation { get; set; }

    public virtual Employee IdEmployeerFkNavigation { get; set; } = null!;
}
