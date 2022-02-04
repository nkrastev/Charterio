namespace Charterio.Services.Data.Ticket
{
    using Charterio.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        int CreateTicket(TicketCreateViewModel input);

        TicketViewModel GetTicketById(int ticketId);

        bool IsTicketIdValid(int ticketId);

        double CalculateTicketPrice(int ticketId);

        string MarkTicketAsPaidviaStripe(int ticketId, string transactionId, string transactionCode, double amount);
    }
}
