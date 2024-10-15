using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Training
{
    public int IdTraining { get; set; }

    public string NameTraining { get; set; } = null!;

    public string? DescriptionTraining { get; set; }

    public int? IdCompanyFk { get; set; }

    public virtual Company? IdCompanyFkNavigation { get; set; }

    public virtual ICollection<TrainingEmployee> TrainingEmployees { get; set; } = new List<TrainingEmployee>();
}
