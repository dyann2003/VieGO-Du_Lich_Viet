using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public int? RoleId { get; set; }

    public string? Gender { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int Status { get; set; }

    public string UserType { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<ServiceProvider> ServiceProviders { get; set; } = new List<ServiceProvider>();

    public virtual ICollection<TourAttendee> TourAttendees { get; set; } = new List<TourAttendee>();

    public virtual ICollection<TourGuide> TourGuides { get; set; } = new List<TourGuide>();

    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();
}
