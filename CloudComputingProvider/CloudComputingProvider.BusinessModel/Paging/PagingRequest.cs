using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.BusinessModel.Paging
{
    public class PagingRequest
    {
        protected const int DefaultPageSize = 10;
        protected const int MaxPageSize = 1000;

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        [Range(1, MaxPageSize)]
        public int MaxResultRecords { get; set; }

        public string? SearchText { get; set; }

        public List<SearchFilter>? SearchFilter { get; set; }

        public List<SearchSorting>? SearchSorting { get; set; }

        public PagingRequest()
        {
            MaxResultRecords = DefaultPageSize;
        }
    }
}
