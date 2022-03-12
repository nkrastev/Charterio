namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Airplane;
    using Charterio.Web.ViewModels.Administration.Airplane;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class AirplaneServiceTests
    {
        [Fact]
        public void GetAllPlanesReturnCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllPlanesReturnCorrectNumber").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Planes.Add(new Plane { Model = "Model 1" });
            dbContext.Planes.Add(new Plane { Model = "Model 2" });
            dbContext.SaveChanges();
            var service = new AirplaneService(dbContext);

            Assert.Equal(2, service.GetAll().Count);
        }

        [Fact]
        public void DeleteReduceSize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteReduceSize").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Planes.Add(new Plane { Model = "Model 1" });
            dbContext.Planes.Add(new Plane { Model = "Model 2" });
            dbContext.SaveChanges();
            var service = new AirplaneService(dbContext);

            service.Delete(1);
            Assert.Single(service.GetAll());
        }

        [Fact]
        public void DeleteDoesNotThrowIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DeleteDoesNotThrowIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Planes.Add(new Plane { Model = "Model 1" });
            dbContext.Planes.Add(new Plane { Model = "Model 2" });
            dbContext.SaveChanges();
            var service = new AirplaneService(dbContext);

            var exception = Record.Exception(() => service.Delete(99));
            Assert.Null(exception);
        }

        [Fact]
        public void EditChangesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditChangesTheData").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Planes.Add(new Plane { Model = "Model 1" });
            dbContext.Planes.Add(new Plane { Model = "Model 2" });
            dbContext.SaveChanges();
            var service = new AirplaneService(dbContext);

            var model = new AirplaneViewModel
            {
                Id = 1,
                Model = "Edited",
            };
            service.Edit(model);
            var editedData = service.GetById(1);
            Assert.Equal("Edited", editedData.Model);
        }

        [Fact]
        public void AddingPlaneIncreaseSizeOfCollection()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AddingPlaneIncreaseSizeOfCollection").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Planes.Add(new Plane { Model = "Model 1" });
            dbContext.Planes.Add(new Plane { Model = "Model 2" });
            dbContext.SaveChanges();
            var service = new AirplaneService(dbContext);

            service.Add(new AirplaneAddViewModel
            {
                Model = "Third",
            });
            var editedData = service.GetAll();
            Assert.Equal(3, editedData.Count);
        }
    }
}
