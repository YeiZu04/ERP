using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Candidate
{
    public int IdCandidate { get; set; }

    public int? IdPersonFk { get; set; }

    public DateTime? ApplicationDateCandidate { get; set; }

    public string? PositionAppliedCandidate { get; set; }

    public int? IdCompanyFk { get; set; }

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual ICollection<FilterCandidate> FilterCandidates { get; set; } = new List<FilterCandidate>();

    public virtual Company? IdCompanyFkNavigation { get; set; }

    public virtual Person? IdPersonFkNavigation { get; set; }
}
