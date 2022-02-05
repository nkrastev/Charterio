namespace Charterio.Services.Data.UptimeRobot
{
    using System.Linq;

    using Charterio.Data;
    using Charterio.Data.Common.Models;
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

        public string GetRatio()
        {
            string result = "100";

            // empty table
            if (!this.db.UptimeRobots.Any())
            {
                result = GetRatioFromApi(this.ApiKey);
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

                if (lastEntry.CreatedOn.AddHours(2) > System.DateTime.UtcNow)
                {
                    // the last entry is older than 2 hours, generate new
                    result = GetRatioFromApi(this.ApiKey);
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

        private static string GetRatioFromApi(string apiKey)
        {
            string result;

            try
            {
                var client = new RestClient("https://api.uptimerobot.com/v2/getMonitors");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", $"api_key={apiKey}&format=json&all_time_uptime_ratio=1", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                dynamic data = JObject.Parse(response.Content);
                result = data.monitors[0].all_time_uptime_ratio;
            }
            catch (System.Exception)
            {
                result = "External Api Error";
            }

            return result;
        }
    }
}
