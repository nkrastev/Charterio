namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; }
    }
}
