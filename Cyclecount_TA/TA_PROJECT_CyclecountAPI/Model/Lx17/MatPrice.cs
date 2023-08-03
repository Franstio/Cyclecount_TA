using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TA_PROJECT_CyclecountAPI.Model.User;

namespace TA_PROJECT_CyclecountAPI.Model.Lx17
{
    public class MatPrice
    {
        public int id { get; set; }
        public int PlantId { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string matnr { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string? matdesc { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? ms { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? uom { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? type { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? pgr { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? mrpc { get; set; }
        public double price { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string cur { get; set; }
        public double per { get; set; }
        public int? idusin { get; set; }
        public int? idusup { get; set; }
        public virtual UserModel? Created_By { get; set; } 
        public virtual UserModel? Updated_By { get; set; } 
        public virtual DeptModel? Plant { get; set; } 
        public DateTime ymd8in { get; set; } = DateTime.Now;
        public DateTime ymd8up { get; set; } = DateTime.Now;
    }
}
