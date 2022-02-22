namespace Charterio.Web.ViewModels.Administration.Airport
{
    public class AirportViewModel
    {
        public int Id { get; set; }

        public string IataCode { get; set; }

        public string Name { get; set; }

        public int UtcPosition { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
    }
}
