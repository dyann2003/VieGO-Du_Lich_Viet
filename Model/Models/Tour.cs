using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string TourCode { get; set; } = null!;

    public int ServiceProviderId { get; set; }

    public string TourName { get; set; } = null!;

    public string? Duration { get; set; }

    public int? GroupSizeMin { get; set; }

    public int? GroupSizeMax { get; set; }

    public string? TourType { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public string? DepartureCity { get; set; }

    public string? Destination { get; set; }

    public string? Policies { get; set; }

    public string? FeaturedImageUrl { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ServiceProvider ServiceProvider { get; set; } = null!;

    public virtual ICollection<TourAssignment> TourAssignments { get; set; } = new List<TourAssignment>();

    public virtual ICollection<TourExclusion> TourExclusions { get; set; } = new List<TourExclusion>();

    public virtual ICollection<TourHighlight> TourHighlights { get; set; } = new List<TourHighlight>();

    public virtual ICollection<TourInclusion> TourInclusions { get; set; } = new List<TourInclusion>();

    public virtual ICollection<TourSchedule> TourSchedules { get; set; } = new List<TourSchedule>();

    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();
}
