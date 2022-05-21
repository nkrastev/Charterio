namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Services.Data.UptimeRobot;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class UptimeRobotServiceTests
    {
        [Fact]
        public void GetRatioUptimeReturnsString()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("UptimeReturnsCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "UptimeApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var service = new UptimeRobotService(dbContext, configuration);

            Assert.IsType<string>(service.GetRatioAsync());
        }

        [Fact]
        public void GetRatioInsertsRecordInDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetRatioInsertsRecordInDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "UptimeApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var service = new UptimeRobotService(dbContext, configuration);
            service.GetRatioAsync();

            var countAfter = dbContext.UptimeRobots.ToList().Count();
            Assert.Equal(1, countAfter);
        }

        [Fact]
        public void LastEntryIsNewerThan2HoursDoNotInsertNewOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("LastEntryIsNewerThan2HoursDoNotInsertNewOne").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "UptimeApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var service = new UptimeRobotService(dbContext, configuration);
            service.GetRatioAsync();

            Assert.Single(dbContext.UptimeRobots.ToList());

            service.GetRatioAsync();
            Assert.Single(dbContext.UptimeRobots.ToList());
        }

        [Fact]
        public void AddNewEntryIfTheLastOneIsOlderThan2Hours()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AddNewEntryIfTheLastOneIsOlderThan2Hours").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "UptimeApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var service = new UptimeRobotService(dbContext, configuration);
            service.GetRatioAsync();

            Assert.Single(dbContext.UptimeRobots.ToList());

            // First entry is inserted. Set created on to yesterday and run service again
            var ratio = dbContext.UptimeRobots.FirstOrDefault();
            ratio.CreatedOn = DateTime.Now.AddDays(-1);
            service.GetRatioAsync();
            Assert.Equal(2, dbContext.UptimeRobots.ToList().Count);
        }
    }
}
