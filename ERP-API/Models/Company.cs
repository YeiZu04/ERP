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

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
