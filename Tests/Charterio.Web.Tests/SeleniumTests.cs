namespace Charterio.Web.Tests
{
    using System;
    using System.Linq;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using Xunit;

    public class SeleniumTests : IClassFixture<SeleniumServerFactory<Startup>>, IDisposable
    {
        private readonly SeleniumServerFactory<Startup> server;
        private readonly IWebDriver browser;

        public SeleniumTests(SeleniumServerFactory<Startup> server)
        {
            this.server = server;
            server.CreateClient();
            var opts = new ChromeOptions();
            opts.AddArguments("--headless");
            opts.AcceptInsecureCertificates = true;
            this.browser = new ChromeDriver(opts);
        }

        [Fact]
        public void FooterOfThePageContainsTosLink()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri);
            Assert.EndsWith("/Home/Tos", this.browser.FindElements(By.CssSelector("footer a")).Last().GetAttribute("href"));
        }

        [Fact]
        public void GuestHasLoginLinkInFlightDetailsPage()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/FlightDetails/1");
            Assert.EndsWith("/identity/account/login", this.browser.FindElements(By.LinkText("logged in")).FirstOrDefault().GetAttribute("href"));
        }

        [Fact]
        public void NotExistingFlightRedirectsToNotFoundPage()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/FlightDetails/99999");
            Assert.EndsWith("/NotFound", this.browser.Url);
        }

        [Fact]
        public void SearchWithNoExistingAirportReturnsNoAvailableFlights()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Search/MAR/CDG/1?StartFlightDate=03%2F27%2F2022%2000%3A00%3A00&EndFlightDate=04%2F30%2F2022%2000%3A00%3A00");
            Assert.Contains("Flights Available: ( 0 )", this.browser.FindElements(By.TagName("h2")).FirstOrDefault().Text);
        }

        [Fact]
        public void SearchWithMissingAirportReturnsUpsPage()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Search/CDG/1?StartFlightDate=03%2F27%2F2022%2000%3A00%3A00&EndFlightDate=04%2F30%2F2022%2000%3A00%3A00");
            Assert.Contains("Ups. Something is wrong.", this.browser.FindElements(By.TagName("h3")).FirstOrDefault().Text);
        }

        [Fact]
        public void TryingToOpenAdministrationForwardsUserToLogin()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Administration/");
            Assert.Contains("/Identity/Account/Login?ReturnUrl=%2FAdministration%2F", this.browser.Url);
        }

        [Fact]
        public void GuestTryingToOpenTicketDoNotLoadsData()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Booking/Ticket?tid=3");
            Assert.Contains("/Identity/Account/Login?ReturnUrl", this.browser.Url);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.server?.Dispose();
                this.browser?.Dispose();
            }
        }
    }
}
