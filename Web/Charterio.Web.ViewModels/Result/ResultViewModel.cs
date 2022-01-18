namespace Charterio.Web.ViewModels.Result
{
    using System;

    public class ResultViewModel
    {
        public int Id { get; set; }

        public string StartApt { get; set; }

        public string EndApt { get; set; }

        public DateTime DepartureDate { get; set; }

        public double Price { get; set; }
    }
}
