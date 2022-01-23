namespace Charterio.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
            const int ITEMS_PER_PAGE = 10;

            var data = new FaqListViewModel()
            {
                ItemsPerPage = ITEMS_PER_PAGE,
                PageNumber = pageNum,
                FaqsList = this.faqService.GetAllFaq(pageNum, ITEMS_PER_PAGE),
                FaqsCount = this.faqService.GetCount(),
            };

            return this.View(data);
        }
    }
}
