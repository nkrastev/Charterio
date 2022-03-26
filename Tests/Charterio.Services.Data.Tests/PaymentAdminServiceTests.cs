namespace Charterio.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Administration;
    using Charterio.Services.Payment;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PaymentAdminServiceTests
    {
        [Fact]
        public void GetAllReturnsListOfPaymentMethods()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllReturnsListOfPaymentMethods").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new PaymentAdministrationService(dbContext);

            dbContext.PaymentMethods.Add(new PaymentMethod { Name = "Stripe", IsActive = true });
            dbContext.PaymentMethods.Add(new PaymentMethod { Name = "Braintree", IsActive = true });
            dbContext.PaymentMethods.Add(new PaymentMethod { Name = "Borica", IsActive = true });

            dbContext.SaveChanges();

            // Act
            var list = service.GetAll();

            // Assert
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void DisableByIdChangesIsActiveStatus()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("DisableByIdChangesIsActiveStatus").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new PaymentAdministrationService(dbContext);

            dbContext.PaymentMethods.Add(new PaymentMethod { Id = 1, Name = "Stripe", IsActive = true });
            dbContext.PaymentMethods.Add(new PaymentMethod { Id = 2, Name = "Braintree", IsActive = true });

            dbContext.SaveChanges();

            // Act
            service.DisableMethodById(1);

            // Assert
            Assert.False(dbContext.PaymentMethods.Where(x => x.Id == 1).FirstOrDefault().IsActive);
        }

        [Fact]
        public void EnableByIdChangesIsActiveStatus()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EnableByIdChangesIsActiveStatus").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new PaymentAdministrationService(dbContext);

            dbContext.PaymentMethods.Add(new PaymentMethod { Id = 1, Name = "Stripe", IsActive = false });
            dbContext.PaymentMethods.Add(new PaymentMethod { Id = 2, Name = "Braintree", IsActive = false });

            dbContext.SaveChanges();

            // Act
            service.EnableMethodById(2);

            // Assert
            Assert.True(dbContext.PaymentMethods.Where(x => x.Id == 2).FirstOrDefault().IsActive);
        }
    }
}
