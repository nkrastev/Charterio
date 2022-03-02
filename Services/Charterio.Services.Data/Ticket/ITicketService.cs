namespace Charterio.Services.Data.Ticket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Charterio.Web.ViewModels.Administration.Ticket;
    using Charterio.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        int CreateTicket(TicketCreateViewModel input);

        TicketViewModel GetTicketById(int ticketId);

        bool IsTicketIdValid(int ticketId);

        double CalculateTicketPrice(int ticketId);

        void MarkTicketAsCancelled(int ticketId);

        ICollection<TicketAdminViewModel> GetAll();

        Task SendConfirmationEmailAsync(int ticketId);
    }
}
