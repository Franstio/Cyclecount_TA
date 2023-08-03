namespace TA_PROJECT_CyclecountAPI.Model.API.Lx17
{
    public class Lx17SAPReq
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ETLIST
        {
            public Model.Lx17.Lx17 item { get; set; } = new Model.Lx17.Lx17();
        }

        public class Item
        {
            public string SIGN { get; set; }
            public string OPTION { get; set; }
            public string LOW { get; set; }
            public string HIGH { get; set; }

        }
        public string IM_ABWEI { get; set; }
        public string IM_DWERT { get; set; }
        public string IM_LGNUM { get; set; }
        public string IM_NIGEZ { get; set; }
        public string IM_WAERS { get; set; }
        public ETLIST ET_LIST { get; set; } = new ETLIST();
        public SIVNUM S_IVNUM { get; set; } = new SIVNUM();
        public SLQNUM S_LQNUM { get; set; } = new SLQNUM(); 
        public SMATNR S_MATNR { get; set; } = new SMATNR(); 

        public class SIVNUM
        {
            public Item item { get; set; }
        }

        public class SLQNUM
        {
            public Item item { get; set; }
        }

        public class SMATNR
        {
            public Item item { get; set; }
        }


    }
}
