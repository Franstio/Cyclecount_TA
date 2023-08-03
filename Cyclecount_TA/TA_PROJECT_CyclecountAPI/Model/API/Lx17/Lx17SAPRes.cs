using TA_PROJECT_CyclecountAPI.Model.Lx17;

namespace TA_PROJECT_CyclecountAPI.Model.API.Lx17
{
    public class Lx17SAPRes
    {
        public class RETURNs
        {
            public string TYPE { get; set; }
            public string ID { get; set; }
            public string NUMBER { get; set; }
            public string MESSAGE { get; set; }
            public string LOG_NO { get; set; }
            public string LOG_MSG_NO { get; set; }
            public string MESSAGE_V1 { get; set; }
            public string MESSAGE_V2 { get; set; }
            public string MESSAGE_V3 { get; set; }
            public string MESSAGE_V4 { get; set; }
            public string PARAMETER { get; set; }
            public int ROW { get; set; }
            public string FIELD { get; set; }
            public string SYSTEM { get; set; }

        }
        public class ET_LISTs
        {
            public IList<Model.Lx17.Lx17> item { get; set; }

        }
        public RETURNs RETURN { get; set; }
        public ET_LISTs ET_LIST { get; set; }
        public string S_IVNUM { get; set; }
        public string S_LQNUM { get; set; }
        public string S_MATNR { get; set; }


    }
}
