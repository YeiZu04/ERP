
using System;
using System.Collections.Generic;

namespace ERP_API.Models;


public partial class User 

{
    public int IdUser { get; set; }

    public string NameUser { get; set; } = null!;

    public string PasswordUser { get; set; } = null!;

    public DateTime? CreationDateUser { get; set; }

    public int? IdPersonFk { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Person? IdPersonFkNavigation { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
