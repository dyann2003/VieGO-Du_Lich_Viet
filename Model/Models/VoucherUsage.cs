using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class VoucherUsage
{
    public int UsageId { get; set; }

    public int DiscountCodeId { get; set; }

    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int TourId { get; set; }

    public DateOnly UsageDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual DiscountCode DiscountCode { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
