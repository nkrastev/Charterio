namespace Charterio.Services.Data.Tests
{
    using Charterio.Data;
    using Charterio.Services.Data.Airport;
    using Charterio.Web.ViewModels.Administration.Airport;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AirportServiceTests
    {
        [Fact]
        public void AddAirportIncreaseSize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AddAirportIncreaseSize").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new AirportService(dbContext);

            service.Add(new Web.ViewModels.Administration.Airport.AirportAddViewModel
            {
                IataCode = "SOF",
                Name = "Sofia",
                UtcPosition = 3,
                Latitude = 1,
                Longtitude = 1,
            });

            Assert.Single(service.GetAll());
        }

        [Fact]
        public void EditAirportReturnCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditAirportReturnCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new AirportService(dbContext);

            service.Add(new Web.ViewModels.Administration.Airport.AirportAddViewModel
            {
                IataCode = "SOF",
                Name = "Sofia",
                UtcPosition = 3,
                Latitude = 1,
                Longtitude = 1,
            });

            var model = new AirportViewModel
            {
                Id = 1,
                IataCode = "BOJ",
                Name = "Burgas",
                UtcPosition = 3,
                Latitude = 1,
                Longtitude = 1,
            };

            service.Edit(model);

            var dataForEdit = service.GetById(1);
            service.Edit(dataForEdit);

            var editedData = service.GetById(1);

            Assert.Equal("BOJ", editedData.IataCode);
            Assert.Equal("Burgas", editedData.Name);
        }

        [Fact]
        public void GetByIdDoNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdDoNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);
            var service = new AirportService(dbContext);

            service.Add(new Web.ViewModels.Administration.Airport.AirportAddViewModel
            {
                IataCode = "SOF",
                Name = "Sofia",
                UtcPosition = 3,
                Latitude = 1,
                Longtitude = 1,
            });

            var exception = Record.Exception(() => service.GetById(99));
            Assert.Null(exception);
        }
    }
}
