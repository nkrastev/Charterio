namespace Charterio.Services.Payment.ViaBraintree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Braintree;
    using Microsoft.Extensions.Configuration;

    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration configuration;

        public BraintreeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<string> MarkTicketAsPaid(int ticketId, string transactionId, double amount)
        {
            throw new NotImplementedException();
        }

        public string ProcessPayment(string stripeToken, string stripeEmail, int ticketId)
        {
            throw new NotImplementedException();
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
    }
}
