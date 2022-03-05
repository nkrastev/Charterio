namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;

    internal class PlanesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Planes.Any())
            {
                return;
            }

            await dbContext.Planes.AddAsync(new Plane { Model = "Airbus A320" });
            await dbContext.Planes.AddAsync(new Plane { Model = "Airbus A319" });
            await dbContext.Planes.AddAsync(new Plane { Model = "McDonnell Douglas - MD82" });
            await dbContext.Planes.AddAsync(new Plane { Model = "Embraer - 190" });
        }
    }
}
