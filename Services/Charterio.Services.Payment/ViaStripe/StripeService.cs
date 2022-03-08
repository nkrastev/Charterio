namespace Charterio.Services.Payment.ViaStripe
{
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Global;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Services.Data.Ticket;
    using Stripe.Checkout;

    public class StripeService : IStripeService
    {
        private readonly ApplicationDbContext db;
        private readonly ITicketService ticketService;
        private readonly ISendGrid emailSender;

        public StripeService(ApplicationDbContext db, ITicketService ticketService, ISendGrid emailSender)
        {
            this.db = db;
            this.ticketService = ticketService;
            this.emailSender = emailSender;
        }

        public string ProcessPayment(string stripeToken, string stripeEmail, int ticketId)
        {
            var domain = "https://localhost:44319";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Name = GlobalConstants.FlightTicket,
                    Quantity = 1,
                    Amount = (long?)(this.ticketService.CalculateTicketPrice(ticketId) * 100),
                    Currency = "EUR",
                    Description = GlobalConstants.StripePaymentDescription,
                  },
                },
                Metadata = new Dictionary<string, string> { { "TicketId", ticketId.ToString() } },
                Mode = "payment",
                SuccessUrl = domain + "/booking/SuccessStripe?sid={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/booking/FailStripe?sid={CHECKOUT_SESSION_ID}",
            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            return session.Url;
        }

        public async Task<string> MarkTicketAsPaid(int ticketId, string transactionId, double amount)
        {
            // Check if payment is not already submitted
            if (this.db.Payments.Where(x => x.TransactionId == transactionId).Any())
            {
                // Payment is already inserted, the user is refreshing the page
                return "OK";
            }

            // Insert Payment
            var payment = new Charterio.Data.Models.Payment
            {
                PaymentMethodId = 1,
                TransactionId = transactionId,
                Amount = amount,
                IsSuccessful = true,
            };

            this.db.Payments.Add(payment);
            this.db.SaveChanges();

            // Get paymend back via transactionId
            var targetPayment = this.db.Payments.Where(x => x.TransactionId == payment.TransactionId).FirstOrDefault();

            // Change ticket status
            var targetTicket = this.db.Tickets.Where(x => x.Id == ticketId).FirstOrDefault();
            if (targetTicket != null && targetPayment != null)
            {
                targetTicket.TicketStatusId = 1;
                targetTicket.PaymentId = targetPayment.Id;
                this.db.SaveChanges();
                await this.ticketService.SendConfirmationEmailAsync(ticketId);
                return "OK";
            }
            else
            {
                return "Fail";
            }
        }
    }
}
