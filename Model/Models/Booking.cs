using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int TourId { get; set; }

    public int ScheduleId { get; set; }

    public int UserId { get; set; }

    public DateOnly BookingDate { get; set; }

    public DateOnly TravelStartDate { get; set; }

    public DateOnly TravelEndDate { get; set; }

    public int NumAdults { get; set; }

    public int? NumChildren { get; set; }

    public string? SpecialRequests { get; set; }

    public decimal TotalPrice { get; set; }

    public string BookingStatus { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public int? PaymentMethodId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public int? DiscountCodeId { get; set; }

    public string? PaymentNotes { get; set; }

    public virtual DiscountCode? DiscountCode { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual TourSchedule Schedule { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;

    public virtual ICollection<TourAttendee> TourAttendees { get; set; } = new List<TourAttendee>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();
}
