using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Curriculum
{
    public int IdCurriculum { get; set; }

    public int? IdEmployeeFk { get; set; }

    public string? PathFileCurriculum { get; set; }

    public DateTime? DateUploaded { get; set; }

    public virtual Employee? IdEmployeeFkNavigation { get; set; }
}
