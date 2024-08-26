using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string? TypeRole { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
