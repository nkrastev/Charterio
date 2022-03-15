namespace Charterio.Services.Data.Tests
{
    using Charterio.Data;
    using Charterio.Services.Data.Api;
    using Charterio.Services.Data.Flight;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FlightApiServiceTests
    {
        [Fact]
        public void FlightApiServiceReturnsData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("FlightApiServiceReturnsData").Options;
            var dbContext = new ApplicationDbContext(options);
            var allotmentService = new AllotmentService(dbContext);
            var service = new FlightApiService(dbContext, allotmentService);

            var data = service.GetData();

            Assert.NotNull(data);
        }
    }
}
