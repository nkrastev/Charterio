namespace Charterio.Web.ViewModels.Administration.Flight
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Web.ViewModels.Administration.Airplane;

    public class FlightAdminDropdownsViewModel
    {
        public ICollection<AirplaneViewModel> AirplaneForDropDown { get; set; } = new List<AirplaneViewModel>();

        public ICollection<CompanyViewModel> CompanyForDropDown { get; set; } = new List<CompanyViewModel>();
    }
}
