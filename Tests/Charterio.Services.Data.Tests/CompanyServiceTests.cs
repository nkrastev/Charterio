namespace Charterio.Services.Data.Tests
{
    using Charterio.Data;
    using Charterio.Services.Data.Company;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CompanyServiceTests
    {
        [Fact]
        public void AddServiceAddsCorrentData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AddServiceAddsCorrentData").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new CompanyService(dbContext);

            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel
            {
                Name = "Test name",
            });

            Assert.Single(service.GetAll());
        }

        [Fact]
        public void GetAllReturnsDataWith2Records()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllReturnsDataWith2Records").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new CompanyService(dbContext);

            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel
            {
                Name = "Test name",
            });
            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel
            {
                Name = "Second Test name",
            });

            Assert.Equal(2, service.GetAll().Count);
        }

        [Fact]
        public void GetByIdReturnsNullIfIdInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsNullIfIdInvalid").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new CompanyService(dbContext);

            Assert.Null(service.GetById(5));
        }

        [Fact]
        public void GetByIdReturnsCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new CompanyService(dbContext);
            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel { Name = "First name", });
            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel { Name = "Second name", });

            Assert.Equal("Second name", service.GetById(2).Name);
        }

        [Fact]
        public void EditByModelChangesValues()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditByModelChangesValues").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new CompanyService(dbContext);
            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel { Name = "First name", });
            service.Add(new Web.ViewModels.Administration.Company.CompanyAddViewModel { Name = "Second name", });

            service.Edit(new Web.ViewModels.Administration.Company.CompanyViewModel { Name = "EditedName", Id = 1 });

            Assert.Equal("EditedName", service.GetById(1).Name);
        }
    }
}
