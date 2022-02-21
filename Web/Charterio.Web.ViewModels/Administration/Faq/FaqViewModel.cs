namespace Charterio.Web.ViewModels.Administration.Faq
{
    using System.ComponentModel.DataAnnotations;

    public class FaqViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
