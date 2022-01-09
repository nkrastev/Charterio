namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}