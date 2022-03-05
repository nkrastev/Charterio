namespace Charterio.Services.Payment.ViaStripe
{
    public interface IStripeService
    {
        Task<string> MarkTicketAsPaid(int ticketId, string transactionId, double amount);

        string ProcessPayment(string stripeToken, string stripeEmail, int ticketId);
    }
}
