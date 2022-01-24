namespace Charterio.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class ContactsViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
