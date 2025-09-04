namespace Hospital_Hub_Portal.Models
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int StartRecord => ((PageNumber - 1) * PageSize) + 1;
        public int EndRecord => Math.Min(PageNumber * PageSize, TotalRecords);
    }

    public class DoctorSearchRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; } = "";
        public string SpecializationFilter { get; set; } = "";
        public string StatusFilter { get; set; } = "";
        public string SortBy { get; set; } = "DoctorName";
        public string SortDirection { get; set; } = "asc";
    }
}
