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
            var data = this.GetDataByPage(pageNum);

            // If pageNum if out of range, get data for first one
            if (data.PagesCount < pageNum)
            {
                data = this.GetDataByPage(1);
            }

            return this.View(data);
        }

        private FaqListViewModel GetDataByPage(int pageNum)
        {
            var data = new FaqListViewModel()
            {
                PageNumber = pageNum,
                FaqsList = this.faqService.GetAllFaq(pageNum),
                FaqsCount = this.faqService.GetCount(),
            };
            return data;
        }
    }
}
