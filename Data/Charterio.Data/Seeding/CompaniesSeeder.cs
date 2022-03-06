namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;

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
            await dbContext.Companies.AddAsync(new Company { Name = "Electra Airways" });
            await dbContext.Companies.AddAsync(new Company { Name = "Jet Time" });
            await dbContext.Companies.AddAsync(new Company { Name = "Corsair" });
            await dbContext.Companies.AddAsync(new Company { Name = "Condor" });
            await dbContext.Companies.AddAsync(new Company { Name = "TUI fly Deutschland" });
        }
    }
}
