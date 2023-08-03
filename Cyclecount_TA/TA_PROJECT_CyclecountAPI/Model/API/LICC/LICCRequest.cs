namespace TA_PROJECT_CyclecountAPI.Model.API.LICC
{
    public class LICCRequest
    {
        public string? B_DAT { get; set; } = String.Empty; //B_DAT == DTTO
        public string? PDATU { get; set; } = String.Empty;//PDATU == DTFORM
        public string? P_COUNT { get; set; } = String.Empty;
        public string? P_EDATU { get; set; } = String.Empty;
        public string? P_LGNUM { get; set; } = String.Empty;
        public string? P_LIAKT { get; set; } = String.Empty;
        public string? P_UNAME { get; set; } = String.Empty;
        public string? P_WERKS { get; set; } = String.Empty;
        public string? V_DAT { get; set; } = String.Empty;
        public Item_ET_LICC ET_LICC { get; set; } = new Item_ET_LICC();
        public Item_S_ABCIN S_ABCIN { get; set; } = new Item_S_ABCIN();
        public Item_S_Matnr S_MATNR { get; set; } = new Item_S_Matnr();

        public class Item_ET_LICC
        {
            public Items item { get; set; } = new Items();

            public class Items
            {
                public string? LGNUM { get; set; } = string.Empty;
                public string? LGTYP { get; set; } = string.Empty;
                public string? LGPLA { get; set; } = string.Empty;
                public string? IDATU { get; set; } = string.Empty;
                public string? WERKS { get; set; } = string.Empty;
                public string? MATNR { get; set; } = string.Empty;
                public string? NIDAT { get; set; } = string.Empty;
                public string? TTEXT { get; set; } = string.Empty;
                public string? ABCIN { get; set; } = string.Empty;

                public string? SALK3 { get; set; } = string.Empty;
                public string? AGEING_DAYS { get; set; } = string.Empty;
                public string? LQNUM { get; set; } = string.Empty;
            }
        }
        public class Item_S_ABCIN
        {
            public Items item { get; set; } = new Items();
            public class Items
            {

                public string? SIGN { get; set; } = string.Empty;
                public string? OPTION { get; set; } = string.Empty;
                public string? LOW { get; set; } = string.Empty;
                public string? HIGH { get; set; } = string.Empty;
            }
        }
        public class Item_S_Matnr
        {
            public Items item { get; set; } = new Items(); 
            public class Items
            {
                public string? SIGN { get; set; } = string.Empty;
                public string? OPTION { get; set; } = string.Empty;
                public string? LOW { get; set; } = string.Empty;
                public string? HIGH { get; set; } = string.Empty;
            }
        }
    }
}
