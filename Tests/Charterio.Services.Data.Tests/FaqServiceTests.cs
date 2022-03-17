namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Faq;
    using Charterio.Web.ViewModels.Administration.Faq;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FaqServiceTests
    {
        [Fact]
        public void FaqCountReturnsCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("FaqCountReturnsCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            Assert.Equal(0, service.GetCount());
            dbContext.Faqs.Add(new Faq { Question = "Test", Answer = "Test" });
            dbContext.SaveChanges();
            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public void FaqGetAllReturnsListOnFirstPage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("FaqGetAllReturnsListOnFirstPage").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            dbContext.Faqs.Add(new Faq { Question = "Test1", Answer = "Test1" });
            dbContext.Faqs.Add(new Faq { Question = "Test2", Answer = "Test2" });
            dbContext.SaveChanges();

            Assert.Equal(2, service.GetCount());
            Assert.Equal(2, service.GetAllFaq(1).Count());
            Assert.Empty(service.GetAllFaq(2));
        }

        [Fact]
        public void DeleteByIdDecreaseCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteByIdDecreaseCount").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            dbContext.Faqs.Add(new Faq { Question = "Test1", Answer = "Test1" });
            dbContext.Faqs.Add(new Faq { Question = "Test2", Answer = "Test2" });
            dbContext.SaveChanges();

            Assert.Equal(2, service.GetCount());
            service.Delete(1);
            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public void EditByModelChangesData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditByModelChangesData").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            dbContext.Faqs.Add(new Faq { Question = "Test1", Answer = "Test1" });
            dbContext.SaveChanges();

            service.Edit(new Web.ViewModels.Administration.Faq.FaqViewModel
            {
                Id = 1,
                Question = "New question",
                Answer = "New answer",
            });

            var target = dbContext.Faqs.Where(x => x.Id == 1).FirstOrDefault();

            Assert.Equal("New question", target.Question);
            Assert.Equal("New answer", target.Answer);
        }

        [Fact]
        public void GetByIdReturnsCorrectModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsCorrectModel").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            dbContext.Faqs.Add(new Faq { Question = "Q1", Answer = "A1" });
            dbContext.SaveChanges();

            var model = service.GetById(1);

            Assert.Equal("Q1", model.Question);
            Assert.Equal("A1", model.Answer);
        }

        [Fact]
        public void GetByIdReturnsNullIfModelIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsNullIfModelIsNotFound").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            Assert.Null(service.GetById(1));
        }

        [Fact]
        public void AddNewFaqIncreaseCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AddNewFaqIncreaseCount").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            service.Add(new Web.ViewModels.Administration.Faq.FaqAddViewModel
            {
                Question = "Q1",
                Answer = "A1",
            });

            Assert.Equal(1, dbContext.Faqs.Count());
        }

        [Fact]

        public void GetAllReturnsListWithFaqViewModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllReturnsListWithFaqViewModel").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new FaqService(dbContext);

            dbContext.Faqs.Add(new Faq { Question = "Q1", Answer = "A1" });
            dbContext.Faqs.Add(new Faq { Question = "Q2", Answer = "A2" });
            dbContext.Faqs.Add(new Faq { Question = "Q3", Answer = "A3" });
            dbContext.SaveChanges();

            var list = service.GetAll();

            Assert.Equal(3, list.Count());
            Assert.IsType<List<FaqViewModel>>(list);
        }
    }
}
