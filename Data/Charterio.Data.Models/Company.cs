namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
    }
}
