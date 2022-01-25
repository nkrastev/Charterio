namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class PaxTitlesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PaxTitles.Any())
            {
                return;
            }

            await dbContext.PaxTitles.AddAsync(new PaxTitle { Title = "Mr.", });
            await dbContext.PaxTitles.AddAsync(new PaxTitle { Title = "Mrs.", });
            await dbContext.PaxTitles.AddAsync(new PaxTitle { Title = "Chd.", });
            await dbContext.PaxTitles.AddAsync(new PaxTitle { Title = "Inf.", });
        }
    }
}
