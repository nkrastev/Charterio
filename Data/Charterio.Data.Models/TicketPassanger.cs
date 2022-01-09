namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Models;

    public class TicketPassanger
    {
        public int Id { get; set; }

        [Required]
        public Ticket Ticket { get; set; }

        public int TicketId { get; set; }

        [Required]
        public PaxTitle PaxTitle { get; set; }

        public int PaxTitleId { get; set; }

        [Required]
        public string PaxFirstName { get; set; }

        [Required]
        public string PaxLastName { get; set; }

        public string DOB { get; set; }
    }
}
