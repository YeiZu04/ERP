using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_API.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string? TypeRole { get; set; }

    [Column("description_role")]
    public string? Description { get; set; } // Mapeo de la columna description_role

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
