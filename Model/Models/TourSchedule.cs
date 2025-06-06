using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class TourSchedule
{
    public int ScheduleId { get; set; }

    public int TourId { get; set; }

    public DateOnly DepartureDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    public decimal Price { get; set; }

    public int AvailableSlots { get; set; }

    public string Status { get; set; } = null!;

    public string? MeetingPoint { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Tour Tour { get; set; } = null!;

    public virtual ICollection<TourAssignment> TourAssignments { get; set; } = new List<TourAssignment>();

    public virtual ICollection<TourAttendee> TourAttendees { get; set; } = new List<TourAttendee>();
}
