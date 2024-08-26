using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class FilterCandidate
{
    public int IdFilterCandidate { get; set; }

    public int? IdFilterFk { get; set; }

    public int? IdCandidateFk { get; set; }

    public virtual Candidate? IdCandidateFkNavigation { get; set; }

    public virtual Filter? IdFilterFkNavigation { get; set; }
}
