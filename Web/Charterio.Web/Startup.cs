namespace Charterio.Web
{
    using System.IO;
    using System.Reflection;

    using Charterio.Data;
    using Charterio.Data.Common;
    using Charterio.Data.Common.Repositories;
    using Charterio.Data.Models;
    using Charterio.Data.Repositories;
    using Charterio.Data.Seeding;
    using Charterio.Services.Data;
    using Charterio.Services.Data.Administration;
    using Charterio.Services.Data.Contacts;
    using Charterio.Services.Data.Flight;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Services.Data.Ticket;
    using Charterio.Services.Data.UptimeRobot;
    using Charterio.Services.Mapping;
    using Charterio.Web.ViewModels;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Stripe;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);
            services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Payment configuration
            StripeConfiguration.ApiKey = this.configuration["StripeApiKey"];

            // Application services
            services.AddTransient<ISendGrid, SendGrid>();
            services.AddTransient<IUptimeRobotService, UptimeRobotService>();

            services.AddTransient<IAdminService, AdminService>();

            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IFaqService, FaqService>();
            services.AddTransient<IContactService, ContactService>();

            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IAllotmentService, AllotmentService>();
            services.AddTransient<ITicketService, TicketService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/NotFound";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("searchForFlight", "Search/{startApt}/{endApt}/{paxCount}", new { controller = "Search", action = "SearchFlight" });
                        endpoints.MapControllerRoute("flightDetail", "FlightDetails/{id}/", new { controller = "Detail", action = "Index" });
                        endpoints.MapControllerRoute("faq", "faq/{pageNum?}", new { controller = "Faq", action = "Index" });
                        endpoints.MapControllerRoute(name: "Administration", pattern: "{area:exists}/{controller=Administration}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
