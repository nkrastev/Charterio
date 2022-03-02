namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;
    using Charterio.Global;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!userManager.Users.Any())
            {
                var userCustomer = new ApplicationUser()
                {
                    UserName = "testuser@charterio.com",
                    Email = "testuser@charterio.com",
                    FirstName = "Test",
                    LastName = "Testov",
                    PhoneNumber = "00112233",
                };
                var userAdministrator = new ApplicationUser()
                {
                    UserName = "administrator@charterio.com",
                    Email = "administrator@charterio.com",
                    FirstName = "Admin",
                    LastName = "Adminov",
                    PhoneNumber = "11223344",
                };

                var password = "Papagal7!";

                var resultCustomer = await userManager.CreateAsync(userCustomer, password);
                var resultAdmin = await userManager.CreateAsync(userAdministrator, password);

                if (resultCustomer.Succeeded)
                {
                    await userManager.AddToRoleAsync(userCustomer, GlobalConstants.CustomerRoleName);
                }

                if (resultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(userAdministrator, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
