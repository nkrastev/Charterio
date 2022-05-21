namespace Charterio.Services.Data.UptimeRobot
{
    using System.Threading.Tasks;

    public interface IUptimeRobotService
    {
        string GetRatioAsync();
    }
}
