using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Candidate
{
    public int IdCandidate { get; set; }

    public int? IdPersonFk { get; set; }

    public DateTime? ApplicationDateCandidate { get; set; }

    public string? PositionAppliedCandidate { get; set; }

    public virtual ICollection<CurriculumCandidate> CurriculumCandidates { get; set; } = new List<CurriculumCandidate>();

    public virtual ICollection<FilterCandidate> FilterCandidates { get; set; } = new List<FilterCandidate>();

    public virtual Person? IdPersonFkNavigation { get; set; }
}
