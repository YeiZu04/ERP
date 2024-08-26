using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Notification
{
    public int IdNotification { get; set; }

    public string? TypeNameNotification { get; set; }

    public string? DescriptionNotification { get; set; }

    public DateTime DateNotification { get; set; }

    public int IdEmployeedFk { get; set; }

    public byte StatusNotification { get; set; }

    public virtual Employee IdEmployeedFkNavigation { get; set; } = null!;
}
