using TA_PROJECT_CyclecountAPI.Model.Lx17;

namespace TA_PROJECT_CyclecountAPI.Model.API.Lx17
{
    public class CountLx17Req
    {
        public string I_CHECK_ONLY { get; set; } = "";
        public string I_COMMIT { get; set; } = "X";
        public CountLx17Req()
        { }
        public class T_LINVs
        {
            public class T_LINV_Items 
            {
                public string LGNUM { get; set; }
                public string IVNUM { get; set; }
                public string IVPOS { get; set; }
                public string LGTYP { get; set; }
                public string LGPLA { get; set; }
                public string PLPOS { get; set; }
                public string MATNR { get; set; }
                public string WERKS { get; set; }
                public string CHARG { get; set; }
                public string SOBKZ { get; set; }
                public string LSONR { get; set; }
                public string BESTQ { get; set; }
                public string WDATU { get; set; }
                public string LENUM { get; set; }
                public string MENGA { get; set; }
                public string ALTME { get; set; }
                public string LQNUM { get; set; }
                public string NANUM { get; set; }
                public string NVERS { get; set; }
                public string ISTAT { get; set; }
                public string IDATU { get; set; }
                public string KZINV { get; set; }
                public string IRNUM { get; set; }
                public string MAKTX { get; set; }
                public string ISEIT { get; set; }
                public string LETYP { get; set; }
                public string KZNUL { get; set; }
                public string VFDAT { get; set; }
                public string LGORT { get; set; }
                public string UNAME { get; set; }
                public string MATNR_EXTERNAL { get; set; } = "";
                public string MATNR_VERSION { get; set; } = "";
                public string MATNR_GUID { get; set; } = "";
            }
            public T_LINV_Items[] item { get; set; } 
        }
        public T_LINVs T_LINV { get; set; } = new T_LINVs();
    }
}
