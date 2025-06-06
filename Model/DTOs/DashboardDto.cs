using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class DashboardDto
    {
        public int TotalBooking { get; set; }
        public int TotalUser { get; set; }
        public decimal PurchaseOrders { get; set; }
        public int Comments { get; set; }

        public Dictionary<int, int> Ratings { get; set; } = new();

        public Dictionary<string, int> BookingStatus { get; set; }


        public Dictionary<int, decimal> SalesByMonth { get; set; }



    }

}
