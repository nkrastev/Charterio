namespace Charterio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Faq;
    using Microsoft.EntityFrameworkCore;

    public class FaqService : IFaqService
    {
        private readonly ApplicationDbContext db;

        public FaqService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<FaqItemViewModel> GetAllFaq(int page, int itemsPerPage)
        {
            var faqs = this
                .db
                .Faqs
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToList()
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new FaqItemViewModel
                {
                    Id = x.Id,
                    Question = x.Question,
                    Answer = x.Answer,
                })
                .ToList();

            return faqs;
        }

        public int GetCount()
        {
            return this.db.Faqs.Count();
        }
    }
}
