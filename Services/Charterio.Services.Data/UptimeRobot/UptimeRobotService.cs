namespace Charterio.Services.Data.UptimeRobot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Charterio.Data;
    using Charterio.Global;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;
    using RestSharp;

    public class UptimeRobotService : IUptimeRobotService
    {
        private readonly ApplicationDbContext db;
        private readonly IConfiguration configuration;

        public UptimeRobotService(ApplicationDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        private string ApiKey => this.configuration["UptimeApiKey"];

        public string GetRatioAsync()
        {
            string result = GlobalConstants.Uptime100Percent;

            // empty table
            if (!this.db.UptimeRobots.Any())
            {
                result = GetRatioFromApiAsync(this.ApiKey);
                this.db.UptimeRobots.Add(new Charterio.Data.Models.UptimeRobot
                {
                    Ratio = result,
                });
                this.db.SaveChanges();
            }

            // there are records, check if they are older than 2 hours
            if (this.db.UptimeRobots.Any())
            {
                var lastEntry = this.db.UptimeRobots.OrderByDescending(x => x.Id).FirstOrDefault();

                if (lastEntry.CreatedOn.AddHours(2) < System.DateTime.UtcNow)
                {
                    // the last entry is older than 2 hours, generate new
                    result = GetRatioFromApiAsync(this.ApiKey);
                    this.db.UptimeRobots.Add(new Charterio.Data.Models.UptimeRobot
                    {
                        Ratio = result,
                    });
                    this.db.SaveChanges();
                }
                else
                {
                    result = lastEntry.Ratio;
                }
            }

            return result;
        }

        private static string GetRatioFromApiAsync(string apiKey)
        {
            string result;

            try
            {
                var client = new HttpClient();
                var values = new Dictionary<string, string>
                  {
                      { "cache-control", "no-cache" },
                      { "content-type", "application/x-www-form-urlencoded" },
                      { "api_key", apiKey },
                      { "all_time_uptime_ratio", "1" },
                  };

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync("https://api.uptimerobot.com/v2/getMonitors", content).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;
                dynamic data = JObject.Parse(responseString);

                result = data.monitors[0].all_time_uptime_ratio;
            }
            catch (System.Exception)
            {
                result = GlobalConstants.UptimeApiError;
            }

            return result;
        }
    }
}
