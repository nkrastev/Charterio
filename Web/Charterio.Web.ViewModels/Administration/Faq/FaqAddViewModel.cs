namespace Charterio.Web.ViewModels.Administration.Faq
{
    using System.ComponentModel.DataAnnotations;

    public class FaqAddViewModel
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
