namespace Charterio.Web.ViewModels.Administration.Flight
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Web.ViewModels.Administration.Airplane;

    public class FlightAdminAddViewModel
    {
        public string Number { get; set; }

        public string PlaneId { get; set; }

        public string CompanyId { get; set; }
    }
}
