namespace Charterio.Web.ViewModels.Ticket
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TicketCreateViewModel
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public string UserId { get; set; }

        public List<TicketPaxCreateViewModel> PaxList { get; set; }
    }
}
