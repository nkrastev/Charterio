namespace Charterio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Administration.Faq;
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

        public void Delete(int id)
        {
            var faq = this.db.Faqs.Where(x => x.Id == id).FirstOrDefault();
            if (faq != null)
            {
                this.db.Faqs.Remove(faq);
                this.db.SaveChanges();
            }
        }

        public void Edit(FaqViewModel model)
        {
            var faq = this.db.Faqs.Where(x => x.Id == model.Id).FirstOrDefault();
            if (faq != null)
            {
                faq.Question = model.Question;
                faq.Answer = model.Answer;

                this.db.SaveChanges();
            }
        }

        public FaqViewModel GetById(int id)
        {
            var faq = this.db.Faqs.Where(x => x.Id == id).Select(x => new FaqViewModel
            {
                Id = x.Id,
                Question = x.Question,
                Answer = x.Answer,
            }).FirstOrDefault();
            if (faq != null)
            {
                return faq;
            }
            else
            {
                return null;
            }
        }

        public List<FaqViewModel> GetAll()
        {
            var faqs = this.db
                .Faqs
                .OrderByDescending(x => x.Id)
                .Select(x => new FaqViewModel
                {
                    Id = x.Id,
                    Question = x.Question,
                    Answer = x.Answer,
                })
                .ToList();
            return faqs;
        }

        public void Add(FaqViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
