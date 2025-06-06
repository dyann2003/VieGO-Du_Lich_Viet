using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class TourDetails
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public int AvailableSlots { get; set; }
    }
}
