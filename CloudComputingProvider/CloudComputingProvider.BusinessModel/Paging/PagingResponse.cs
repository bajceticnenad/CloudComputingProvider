namespace CloudComputingProvider.BusinessModel.Paging
{
    public class PagingResponse<T> : Response<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public PagingResponse(int totalRecords, int skipCount, int maxResultRecords)
        {
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling((double)skipCount / maxResultRecords) + 1;
            CurrentPage = (int)Math.Ceiling((double)totalRecords / maxResultRecords);
        }
    }
}
