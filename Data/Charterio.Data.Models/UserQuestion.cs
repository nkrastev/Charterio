namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsAnswered { get; set; }
    }
}
