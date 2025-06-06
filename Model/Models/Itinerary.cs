using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Itinerary
{
    public int ItineraryId { get; set; }

    public int TourId { get; set; }

    public int DayNumber { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Location { get; set; }

    public string Description { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
