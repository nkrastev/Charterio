namespace Charterio.Services.Data.Ticket
{
    using System.Threading.Tasks;

    using Charterio.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        int CreateTicket(TicketCreateViewModel input);

        TicketViewModel GetTicketById(int ticketId);

        bool IsTicketIdValid(int ticketId);

        double CalculateTicketPrice(int ticketId);

        Task<string> MarkTicketAsPaidviaStripe(int ticketId, string transactionId, string transactionCode, double amount);
    }
}
