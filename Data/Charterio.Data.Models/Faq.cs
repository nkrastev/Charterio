namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Faq
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
