using System;
using System.Collections.Generic;

namespace ERP_API.Models;

public partial class Filter
{
    public int IdFilter { get; set; }

    public string? NameFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public DateTime? DateFilter { get; set; }

    public byte? Status { get; set; }

    public string? ObservationAboutCandidate { get; set; }

    public virtual ICollection<FilterCandidate> FilterCandidates { get; set; } = new List<FilterCandidate>();
}
