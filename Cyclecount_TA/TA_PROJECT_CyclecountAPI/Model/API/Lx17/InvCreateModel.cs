namespace TA_PROJECT_CyclecountAPI.Model.API.Lx17
{
    public class InvCreatemodel
    {
        public InvCreatemodel(string uname,string lgtyp,string lgnum)
        {
            this.I_LGNUM = lgnum;
            this.I_LGTYP = lgtyp;
            this.I_UNAME = uname;

        }
        public class T_LAGPs
        {

            public class items_T_LAGPs
            {
                public string LGNUM { get; set; }
                public string LGTYP { get; set; }
                public string LGPLA { get; set; }
            }
            public items_T_LAGPs[] item { get; set; }
        }

        public class T_LQUAs
        {

            public class items_T_LQUAs
            {
                public string LGNUM { get; set; }
                public string LQNUM { get; set; }
            }
            public items_T_LQUAs[] item { get; set; }
        }
        public string I_ANPLB { get; set; } = "5";
        public string I_IRNUM { get; set; } = "";
        public string I_KZINV { get; set; } = "CC";
        public string I_LGNUM { get; set; } = "";
        public string I_LGTYP { get; set; } = "";
        public string I_LIAKT { get; set; } = "X";
        public string I_PDATU { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        public string I_UNAME { get; set; } = "";
        public T_LAGPs T_LAGP { get; set; } = new T_LAGPs();
        public T_LQUAs T_LQUA { get; set; } = new T_LQUAs();
    }
}
