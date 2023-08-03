using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TA_PROJECT_CyclecountAPI.Model.User;

namespace TA_PROJECT_CyclecountAPI.Model.LICC
{
    public class LICC
    {
        public int? id { get; set; }

        [Column(TypeName = "nvarchar(48)")]
        public string? LGNUM { get; set; }

        [Column(TypeName = "nvarchar(48)")]
        public string? LGTYP { get; set; }

        [Column(TypeName = "nvarchar(48)")]
        public string? LGPLA { get; set; }

        [Column(TypeName = "nvarchar(48)")]
        public string? IDATU { get; set; }

        [Column(TypeName = "nvarchar(48)")]
        public string? WERKS { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? MATNR { get; set; }

        [Column(TypeName = "nvarchar(48)")]
        public string? NIDAT { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? TTEXT { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? ABCIN { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? SALK3 { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? AGEING_DAYS { get; set; }
        [Column(TypeName = "nvarchar(48)")]
        public string? LQNUM { get; set; }
        public int? Idusin { get; set; }
        public int? Idusup { get; set; }
        public int PlantId { get; set; }
        public DateTime Ymd8in { get; set; } = DateTime.Now;
        public DateTime Ymd8up { get; set; } = DateTime.Now;

        public virtual UserModel? InsertedBy { get; set; } 
        public virtual UserModel? UpdatedBy { get; set; } 
        public virtual DeptModel? Plant { get; set; } 


    }
}
