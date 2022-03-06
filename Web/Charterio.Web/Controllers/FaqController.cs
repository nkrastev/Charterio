namespace Charterio.Web.Controllers
{
    using Charterio.Global;
    using Charterio.Services.Data;
    using Charterio.Web.ViewModels.Faq;
    using Microsoft.AspNetCore.Mvc;

    public class FaqController : Controller
    {
        private readonly IFaqService faqService;

        public FaqController(IFaqService faqService)
        {
            this.faqService = faqService;
        }

        public IActionResult Index(int pageNum = 1)
        {
            var data = new FaqListViewModel()
            {
                PageNumber = pageNum,
                FaqsList = this.faqService.GetAllFaq(pageNum, GlobalConstants.ItemsPerPage),
                FaqsCount = this.faqService.GetCount(),
            };

            return this.View(data);
        }
    }
}
