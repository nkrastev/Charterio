namespace Charterio.Web.ViewModels.Result
{
    using System;

    public class ResultViewModel
    {
        public int Id { get; set; }

        public string StartAptName { get; set; }

        public int StartAptUtc { get; set; }

        public string EndAptName { get; set; }

        public DateTime FlightStartDate { get; set; }

        public int AvailableSeats { get; set; }

        public double Price { get; set; }
    }
}
