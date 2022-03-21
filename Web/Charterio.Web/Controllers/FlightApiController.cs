namespace Charterio.Web.Controllers
{
    using System.Collections.Generic;

    using Charterio.Services.Data.Api;
    using Charterio.Web.ViewModels.Api;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/available-flights")]
    [ApiController]
    public class FlightApiController : ControllerBase
    {
        private readonly IFlightApiService apiService;

        public FlightApiController(IFlightApiService apiService)
        {
            this.apiService = apiService;
        }

        // GET api/available-flights
        [HttpGet]
        public ActionResult<ICollection<ApiViewModel>> Get()
        {
            return this.apiService.GetData();
        }
    }
}
