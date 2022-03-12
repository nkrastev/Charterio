namespace Charterio.Services.Data.Tests
{
    using Charterio.Data;
    using Charterio.Services.Data.Airport;
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
    }
}
