using TA_PROJECT_CyclecountAPI.Model.User;

namespace TA_PROJECT_CyclecountAPI.Model.Lx17
{
    public class MatClass
    {
        public int id { get; set; }
        public int PlantId { get; set; }
        public string matnr { get; set; }
        public string abcin { get; set; }
        public string fmr { get; set; }
        public int? idusin { get; set; }
        public int? idusup { get; set; }
        public virtual UserModel? Created_By { get; set; } 
        public virtual UserModel? Updated_By { get; set; } 
        public virtual DeptModel? Plant { get; set; } 
        public DateTime ymd8in { get; set; } = DateTime.Now;
        public DateTime ymd8up { get; set; } = DateTime.Now;
    }
}
