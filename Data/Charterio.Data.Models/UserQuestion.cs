namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPhone { get; set; }

        public bool IsAnswered { get; set; }
    }
}
