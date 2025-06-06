using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; } = string.Empty;

        public DateOnly BookingDate { get; set; }
        public DateOnly TravelStartDate { get; set; }
        public DateOnly TravelEndDate { get; set; }

        public int NumAdults { get; set; }
        public int? NumChildren { get; set; }

        public decimal TotalPrice { get; set; }

        public string BookingStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
    }


}
