using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Vacation
{
    public int IdVacations { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public byte? Approved { get; set; }

    public int? DaysTaken { get; set; }

    public int? IdEmployeeFk { get; set; }

    public virtual Employee? IdEmployeeFkNavigation { get; set; }
}
