using TA_PROJECT_CyclecountAPI.Model.Lx17;

namespace TA_PROJECT_CyclecountAPI.Model.API.Lx17
{
    public class InvCreateResponse
    {
        public class Records
        {
            public class T_LINVs
            {
                public Model.Lx17.Lx17[] item_T_LINV { get; set; }
            }
            public T_LINVs T_LINV { get; set; }
        }
        public Records Record { get; set; }
    }
    public class InvCreateResponseSingle
    {
        public class Records
        {
            public class T_LINVs
            {
                public Model.Lx17.Lx17 item_T_LINV { get; set; }
            }
            public T_LINVs T_LINV { get; set; }
        }
        public Records Record { get; set; }
    }
}
