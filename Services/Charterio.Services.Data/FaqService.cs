namespace Charterio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Faq;

    public class FaqService : IFaqService
    {
        private readonly ApplicationDbContext db;

        public FaqService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public FaqListViewModel GetAllFaq()
        {
            var data = this.db.Faqs.ToList();
            var faqs = new FaqListViewModel
            {
                FaqList = new List<FaqItemViewModel>(),
            };

            foreach (var item in data)
            {
                var faqItem = new FaqItemViewModel
                {
                    Id = item.Id,
                    Question = item.Question,
                    Answer = item.Answer,
                };
                faqs.FaqList.Add(faqItem);
            }

            return faqs;
        }
    }
}
