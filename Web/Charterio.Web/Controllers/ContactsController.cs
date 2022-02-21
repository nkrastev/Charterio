namespace Charterio.Web.Controllers
{
    using Charterio.Services.Data.Contacts;
    using Charterio.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IContactService contactsService;

        public ContactsController(IContactService contactsServicе)
        {
            this.contactsService = contactsServicе;
        }

        public IActionResult Index()
        {
            ContactsViewModel prefilledData = new();
            if (this.User.Identity.IsAuthenticated)
            {
                var userData = this.contactsService.GetAspNetUserByUserName(this.User.Identity.Name);
                prefilledData.Email = userData.Email;
                prefilledData.Phone = userData.Phone;
                prefilledData.Name = userData.Fullname;
            }

            return this.View(prefilledData);
        }

        [HttpPost]
        public IActionResult Index(ContactsViewModel inputData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputData);
            }

            this.contactsService.SaveUserQuestion(inputData);

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
