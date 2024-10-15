using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Company
{
    public int IdCompany { get; set; }

    public string NameCompany { get; set; } = null!;

    public string CodeCompany { get; set; } = null!;

    public string? DescriptionCompany { get; set; }

    public string? LocationCompany { get; set; }

    public string? UrlCompany { get; set; }

    public byte StatusCompany { get; set; }

    public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Filter> Filters { get; set; } = new List<Filter>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();

    public virtual ICollection<Warning> Warnings { get; set; } = new List<Warning>();

    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
}
