namespace Charterio.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TicketViewModel
    {
        public int TicketId { get; set; }

        public string TicketCode { get; set; }

        public int TicketStatusId { get; set; }

        public string UserId { get; set; }

        public string StartAptName { get; set; }

        public string EndAptName { get; set; }

        public DateTime StartInLocal { get; set; }

        public DateTime EndInLocal { get; set; }

        public List<TicketPaxViewModel> PaxList { get; set; }
    }
}
