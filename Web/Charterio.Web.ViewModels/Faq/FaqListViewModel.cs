namespace Charterio.Web.ViewModels.Faq
{
    using System;
    using System.Collections.Generic;

    public class FaqListViewModel
    {
        public IEnumerable<FaqItemViewModel> FaqsList { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((decimal)this.FaqsCount / this.ItemsPerPage);

        public int FaqsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
