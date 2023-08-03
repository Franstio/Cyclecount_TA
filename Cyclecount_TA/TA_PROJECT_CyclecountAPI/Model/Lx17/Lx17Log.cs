using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TA_PROJECT_CyclecountAPI.Model.User;

namespace TA_PROJECT_CyclecountAPI.Model.Lx17
{
    public class Lx17Log
    {
        public int? Id { get; set; }
        [Column(TypeName="nvarchar(16)")]
        public string Abcin { get; set; }
        public int? Mandt { get; set; }
        public int? Lgnum { get; set; } = 0;
        [Column(TypeName = "nvarchar(128)")]
        public string? Ivnum { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Ivpos { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Lqnum { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Nanum { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Istat { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Nvers { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Iseit { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Lgtyp { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Lgpla { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Plpos { get; set; } = "";

        [Column(TypeName = "nvarchar(128)")]
        public string Matnr { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Werks { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        public string Charg { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Letyp { get; set; } = "";
        public double? Anzle { get; set; }
        public double? Menge { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Meins { get; set; } = "";
        public double? Menga { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Altme { get; set; } = "";
        public int? Umrez { get; set; }
        public int? Umren { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Bestq { get; set; } = "";
        [Column(TypeName = "nvarchar(48)")]
        public string Sobkz { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Sonum { get; set; } = "";
        [Column(TypeName = "nvarchar(24)")]
        public string Wdatu { get; set; } = "";
        public double? Gesme { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public string Idatu { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Kzinv { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Irnum { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Tanum { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Tapos { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Lenum { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Vfdat { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Lgort { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Uname { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Quinv { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Idqua { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Dbgez { get; set; } = "";
        public int? Key { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Lvorm { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Maktx { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Kreuz { get; set; } = "";
        public int? Pagno { get; set; }
        public int? Linno { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Bemer { get; set; } = "";
        public double? Abwei { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Vzchn { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Ausbu { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Bwkey { get; set; } = "";

        [Column(TypeName = "nvarchar(128)")]
        public string Bukrs { get; set; } = "";

        [Column(TypeName = "nvarchar(128)")]
        public string Waers { get; set; } = "";

        [Column(TypeName = "nvarchar(12)")]
        public string Vprsv { get; set; } = "";
        public double? Verpr { get; set; }
        public double? Stprs { get; set; }
        public int? Peinh { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Cwert { get; set; } = "";
        public double? Dwert { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Noval { get; set; } = "";
        [Column(TypeName = "nvarchar(128)")]
        public string Lsonr { get; set; } = "";
        [Column(TypeName = "nvarchar(48)")]
        public string Cntstatus { get; set; } = "";
        [Column(TypeName = "nvarchar(48)")]
        public string Statustype { get; set; } = "LX17";
        public int? Idusin { get; set; }
        public int? Idusup { get; set; }
        public DateTime Ymd8Log { get; set; } = DateTime.Now;
        public int? Logged_UserId { get; set; }
        public int PlantID { get; set; }
        public int? Lx17Id { get; set; }
        public bool FromLICC { get; set; } = true;
        public DateTime Ymd8in { get; set; } = DateTime.Now;
        public DateTime Ymd8up { get; set; } = DateTime.Now;
        public virtual Lx17? ReferencedLx17 { get; set; } 
        public virtual UserModel? InsertedBy { get; set; } 
        public virtual UserModel? Counter { get; set; } 
        public virtual DeptModel? Plant { get; set; } 
        public virtual UserModel? Logged_User { get; set; } 
        public int Action { get; set; }= 0;// -1: Toggle Recount 0: Added To Db;   1: Created IVNUM; 2: Counted  3: Recounted
    }

}
