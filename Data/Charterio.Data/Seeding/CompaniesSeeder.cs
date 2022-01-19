namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class CompaniesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Companies.Any())
            {
                return;
            }

            await dbContext.Companies.AddAsync(new Company { Name = "European Air Charter" });
            await dbContext.Companies.AddAsync(new Company { Name = "Bulgaria Air" });
            await dbContext.Companies.AddAsync(new Company { Name = "Balkan Holidays" });
            await dbContext.Companies.AddAsync(new Company { Name = "AIR LUBO" });

        }
    }
}
