namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class PaymentMethodsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PaymentMethods.Any())
            {
                return;
            }

            await dbContext.PaymentMethods.AddAsync(new PaymentMethod { Name = "Stripe", IsActive = true });
            await dbContext.PaymentMethods.AddAsync(new PaymentMethod { Name = "Paypal", IsActive = false });
        }
    }
}
