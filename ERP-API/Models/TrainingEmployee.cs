using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class TrainingEmployee
{
    public int IdTrainingEmployee { get; set; }

    public int IdTrainingFk { get; set; }

    public int IdEmployeeFk { get; set; }

    public virtual Employee IdEmployeeFkNavigation { get; set; } = null!;

    public virtual Training IdTrainingFkNavigation { get; set; } = null!;
}
