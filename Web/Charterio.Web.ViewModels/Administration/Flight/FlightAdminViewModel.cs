namespace Charterio.Web.ViewModels.Administration.Flight
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Airplane;

    public class FlightAdminViewModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Plane { get; set; }

        public string Company { get; set; }

        public ICollection<AirplaneViewModel> AirplaneForDropDown { get; set; } = new List<AirplaneViewModel>();

        public ICollection<CompanyViewModel> CompanyForDropDown { get; set; } = new List<CompanyViewModel>();
    }
}
