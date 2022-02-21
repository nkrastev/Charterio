namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Services.Data;
    using Charterio.Web.ViewModels.Administration.Faq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class FaqController : Controller
    {
        private readonly IFaqService faqService;

        public FaqController(IFaqService faqService)
        {
            this.faqService = faqService;
        }

        public IActionResult Index()
        {
            var model = new FaqListViewModel();
            model.Faqs = this.faqService.GetAll();
            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(FaqAddViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.faqService.Add(model);
            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var faq = this.faqService.GetById(id);
            return this.View(faq);
        }

        [HttpPost]
        public IActionResult Edit(FaqViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.faqService.Edit(model);

            return this.RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            this.faqService.Delete(id);
            return this.RedirectToAction("Index");
        }
    }
}
