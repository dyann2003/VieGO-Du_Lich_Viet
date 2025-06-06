using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CheckoutViewModel
    {
        public ContactInfoModel ContactInfo { get; set; } = new ContactInfoModel();
        public PassengerInfoModel PassengerInfo { get; set; } = new PassengerInfoModel();
        public string DiscountCode { get; set; }
        public string PaymentMethod { get; set; }
        public TourInfoModel TourInfo { get; set; } = new TourInfoModel();
    }

    public class ContactInfoModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class PassengerInfoModel
    {
        public int Quantity { get; set; } = 2;
        public string SpecialRequests { get; set; }
    }

    public class TourInfoModel
    {
        public string Name { get; set; } = "Bali 4D3N: Explore Paradise Island";
        public string StartDate { get; set; } = "Flexible";
        public string EndDate { get; set; } = "Flexible";
        public int AvailableSeats { get; set; } = 10;
        public string Price { get; set; } = "$250";

    }
}
