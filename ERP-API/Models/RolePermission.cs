using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class RolePermission
{
    public int IdRolePermission { get; set; }

    public int? IdRoleFk { get; set; }

    public int? IdPermissionFk { get; set; }

    public virtual Permission? IdPermissionFkNavigation { get; set; }

    public virtual Role? IdRoleFkNavigation { get; set; }
}
