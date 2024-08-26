using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Session
{
    public int IdSession { get; set; }

    public string? TokenSession { get; set; }

    public DateTime? CreationDateSession { get; set; }

    public DateTime? ExpirationDateSession { get; set; }

    public DateTime? UpdateDateSession { get; set; }

    public int? IdUserFk { get; set; }

    public byte? StatusSession { get; set; }

    public virtual User? IdUserFkNavigation { get; set; }
}
