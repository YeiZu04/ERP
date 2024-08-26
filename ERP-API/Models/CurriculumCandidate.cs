using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class CurriculumCandidate
{
    public int IdCurriculumCandidate { get; set; }

    public int? IdCandidateFk { get; set; }

    public string? PathCurriculumCandidate { get; set; }

    public virtual Candidate? IdCandidateFkNavigation { get; set; }
}
