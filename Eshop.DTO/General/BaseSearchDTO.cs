namespace Eshop.DTO.General
{
    public class BaseSearchDTO
    {
        public BaseSearchDTO() { }

        private string searchTerm = string.Empty;
        public int Page { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public string? OrderByField { get; set; } = string.Empty;
        public bool OrderByDescending { get; set; } = false;
        public string SearchTerm
        {
            get { return searchTerm; }
            set { searchTerm = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }

    public class BaseSearchByIdDTO : BaseSearchDTO
    {
        public Guid Id { get; set; }
    }

}
