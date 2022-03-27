namespace Charterio.Web.ViewModels.Api
{
    public class ApiViewModel
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string DepartureUtc { get; set; }

        public string ArrivalUtc { get; set; }

        public int AvailableSeats { get; set; }

        public double PricePerSeat { get; set; }
    }
}
