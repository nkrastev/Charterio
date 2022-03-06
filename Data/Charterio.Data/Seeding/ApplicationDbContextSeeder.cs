namespace Charterio.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new AirportsSeeder(),
                              new CompaniesSeeder(),
                              new PlanesSeeder(),
                              new FlightsSeeder(),
                              new CurrenciesSeeder(),
                              new OfferSeeder(),
                              new FaqsSeeder(),
                              new UsersSeeder(),
                              new PaymentMethodsSeeder(),
                              new PaymentsSeeder(),
                              new TicketStatusesSeeder(),
                              new TicketIssuersSeeder(),
                              new TicketsSeeder(),
                              new TicketPassengersSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
