using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class TourExclusion
{
    public int ExclusionId { get; set; }

    public int TourId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
