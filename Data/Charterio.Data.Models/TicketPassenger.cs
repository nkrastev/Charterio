namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Models;

    public class TicketPassenger
    {
        public int Id { get; set; }

        [Required]
        public Ticket Ticket { get; set; }

        public int TicketId { get; set; }

        [Required]
        public string PaxTitle { get; set; }

        [Required]
        public string PaxFirstName { get; set; }

        [Required]
        public string PaxLastName { get; set; }

        public string DOB { get; set; }
    }
}
