using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class UserRole
{
    public int IdUserRole { get; set; }

    public int? IdRoleFk { get; set; }

    public int? IdUserFk { get; set; }

    public virtual Role? IdRoleFkNavigation { get; set; }

    public virtual User? IdUserFkNavigation { get; set; }
}
