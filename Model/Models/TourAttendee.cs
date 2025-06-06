using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class TourAttendee
{
    public int AttendeeId { get; set; }

    public int BookingId { get; set; }

    public int ScheduleId { get; set; }

    public int? UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? AttendanceStatus { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual TourSchedule Schedule { get; set; } = null!;

    public virtual User? User { get; set; }
}
