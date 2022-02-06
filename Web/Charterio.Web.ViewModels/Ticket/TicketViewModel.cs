namespace Charterio.Web.ViewModels.Ticket
{
    using System.Collections.Generic;

    public class TicketViewModel
    {
        public int TicketId { get; set; }

        public string TicketCode { get; set; }

        public string UserId { get; set; }

        public string StartAptName { get; set; }

        public string EndAptName { get; set; }

        public string StartInUtc { get; set; }

        public string EndInUtc { get; set; }

        public List<TicketPaxViewModel> PaxList { get; set; }
    }
}
