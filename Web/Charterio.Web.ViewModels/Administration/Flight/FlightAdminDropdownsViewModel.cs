namespace Charterio.Web.ViewModels.Administration.Flight
{
    using System;
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Airplane;
    using Charterio.Web.ViewModels.Administration.Company;

    public class FlightAdminDropdownsViewModel
    {
        public ICollection<AirplaneViewModel> AirplaneForDropDown { get; set; } = new List<AirplaneViewModel>();

        public ICollection<CompanyViewModel> CompanyForDropDown { get; set; } = new List<CompanyViewModel>();
    }
}
