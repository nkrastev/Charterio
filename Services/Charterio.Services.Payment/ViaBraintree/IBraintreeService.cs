namespace Charterio.Services.Payment.ViaBraintree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Braintree;
    using Charterio.Web.ViewModels.Booking;

    public interface IBraintreeService
    {
        IBraintreeGateway GetGateway();

        Task<string> MarkTicketAsPaid(int ticketId, string transactionId, double amount);

        string ProcessPayment(BraintreeBookingViewModel model);
    }
}
