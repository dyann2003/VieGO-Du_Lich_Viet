using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class TourGuide
{
    public int GuideId { get; set; }

    public int? UserId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string Status { get; set; } = null!;

    public string GuideType { get; set; } = null!;

    public string? Bio { get; set; }

    public virtual ICollection<TourAssignment> TourAssignments { get; set; } = new List<TourAssignment>();

    public virtual User? User { get; set; }
}
