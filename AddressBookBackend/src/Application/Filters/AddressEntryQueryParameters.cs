namespace AddressBook.src.Application.Filters
{
    public class AddressEntryQueryParameters
    {
        public int UserId { get; set; }
        public int Page { get; set; } = 1; 
        public int PageSize { get; set; } = 10; 
        public string? Search { get; set; } 
        public int? JobId { get; set; } 
        public int? DepartmentId { get; set; } 
        public DateOnly? FromDate { get; set; } 
        public DateOnly? ToDate { get; set; } 
        public string SortBy { get; set; } = "FullName"; 
        public string SortOrder { get; set; } = "asc"; 
    }
}
