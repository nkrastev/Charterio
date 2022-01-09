namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PaxTitle
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}