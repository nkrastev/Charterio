namespace Charterio.Services.Payment.ViaBraintree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Braintree;
    using Charterio.Data;
    using Charterio.Global;
    using Charterio.Services.Data.Ticket;
    using Charterio.Web.ViewModels.Booking;
    using Microsoft.Extensions.Configuration;

    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration configuration;
        private readonly ITicketService ticketService;
        private readonly ApplicationDbContext db;

        public BraintreeService(IConfiguration configuration, ITicketService ticketService, ApplicationDbContext db)
        {
            this.configuration = configuration;
            this.ticketService = ticketService;
            this.db = db;
        }

        public string ProcessPayment(BraintreeBookingViewModel model)
        {
            var gateway = this.GetGateway();
            var ticket = model.TicketId;
            var price = this.ticketService.CalculateTicketPrice(ticket);

            var request = new TransactionRequest
            {
                Amount = (decimal)price,
                OrderId = $"{GlobalConstants.Ticket} {model.TicketId}",
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                },
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            // Proceed with payment
            if (result.IsSuccess())
            {
                // Insert payment, send confirmation and redirect
                var markingStatus = this.MarkTicketAsPaid(ticket, result.Target.GraphQLId, price);
                return "/Booking/SuccessBraintree";
            }
            else
            {
                return "/Booking/FailBraintree";
            }
        }

        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = this.configuration["BraintreeGateway:MerchantId"],
                PublicKey = this.configuration["BraintreeGateway:PublicKey"],
                PrivateKey = this.configuration["BraintreeGateway:PrivateKey"],
            };

            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return this.CreateGateway();
        }

        public async Task<string> MarkTicketAsPaid(int ticketId, string transactionId, double amount)
        {
            // Check if payment is not already submitted
            if (this.db.Payments.Where(x => x.TransactionId == transactionId).Any())
            {
                // Payment is already inserted, the user is refreshing the page
                return "OK";
            }

            // Insert Payment, Braintree
            var payment = new Charterio.Data.Models.Payment
            {
                PaymentMethodId = 2,
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
