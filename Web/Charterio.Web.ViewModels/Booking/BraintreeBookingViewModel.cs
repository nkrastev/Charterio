namespace Charterio.Web.ViewModels.Booking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BraintreeBookingViewModel
    {
        public int TicketId { get; set; }

        public string Nonce { get; set; }
    }
}
