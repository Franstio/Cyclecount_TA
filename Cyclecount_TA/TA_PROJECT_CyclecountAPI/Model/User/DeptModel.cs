namespace TA_PROJECT_CyclecountAPI.Model.User
{
    public class DeptModel
    {
        public int Id { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public string WERKS { get; set; } = string.Empty;
        public string LGNUM { get; set; } = string.Empty;
        public int Mltp { get; set; } = 1;//Multiplier
        public int Idusin { get; set; }
        public int Idusup { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Last_Updated { get; set; } = DateTime.Now;

        
    }
}
