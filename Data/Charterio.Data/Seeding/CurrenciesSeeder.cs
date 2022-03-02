namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;
    using global::Data.Models;

    internal class CurrenciesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Currencies.Any())
            {
                return;
            }

            await dbContext.Currencies.AddAsync(
                new Currency { Code = "EUR", Name = "European Euro", Ratio = 1, IsActive = true });
        }
    }
}
