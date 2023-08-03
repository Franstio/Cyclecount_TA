namespace TA_PROJECT_CyclecountAPI.ViewModel
{
    public class Lx17MaterialFilterModel
    {
        public DateTime dFrom { get; set; } = DateTime.Now.AddDays(-7);
        public DateTime dTo { get; set; } = DateTime.Now.AddDays(7);
        public string? Search { get; set; } = "";
        public string? Lgtyp { get; set; } = "";
        public int page { get; set; } = 1;
        public int pagesize { get; set; } = 10;
    }
}
