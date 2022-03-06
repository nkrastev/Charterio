namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Faq
    {
        public int Id { get; init; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
