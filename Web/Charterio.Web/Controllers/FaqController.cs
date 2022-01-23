namespace Charterio.Web.Controllers
{
    using Charterio.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class FaqController : Controller
    {
        private readonly IFaqService faqService;

        public FaqController(IFaqService faqService)
        {
            this.faqService = faqService;
        }

        public IActionResult Index()
        {
            return this.View(this.faqService.GetAllFaq());
        }
    }
}
