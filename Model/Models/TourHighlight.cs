using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class TourHighlight
{
    public int HighlightId { get; set; }

    public int TourId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
