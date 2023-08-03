namespace TA_PROJECT_CyclecountAPI.ViewModel
{
    public class PaginationModel<T> where T : class
    {
        public List<T> Data { get; set; } = new List<T>(0);
        public int TotalData { get; set; }
        public int PageNum { get; set; }
        public bool firstPage { get; set; } = false;
        public bool lastPage { get; set; } = false;
    }
}
