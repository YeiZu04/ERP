using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Permission
{
    public int IdPermission { get; set; }

    public string? NamePermission { get; set; }

    public string? DescriptionPermission { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
