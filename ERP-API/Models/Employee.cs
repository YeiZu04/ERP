using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public DateTime HiringDateEmployee { get; set; }

    public double NetSalaryEmployee { get; set; }

    public string? PositionEmployee { get; set; }

    public string? DepartmentEmployee { get; set; }

    public int? VacationsEmployee { get; set; }

    public int? IdUserFk { get; set; }

    public int? IdCompanyFk { get; set; }

    public virtual ICollection<BenefitsEmployee> BenefitsEmployees { get; set; } = new List<BenefitsEmployee>();

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual Company? IdCompanyFkNavigation { get; set; }

    public virtual User? IdUserFkNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<TrainingEmployee> TrainingEmployees { get; set; } = new List<TrainingEmployee>();

    public virtual ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();

    public virtual ICollection<Warning> Warnings { get; set; } = new List<Warning>();

    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
}
