namespace Charterio.Services.Data.Api
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Api;

    public interface IFlightApiService
    {
        List<ApiViewModel> GetData();
    }
}
