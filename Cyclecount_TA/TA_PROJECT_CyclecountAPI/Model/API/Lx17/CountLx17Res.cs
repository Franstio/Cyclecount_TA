namespace TA_PROJECT_CyclecountAPI.Model.API.Lx17
{
    public class CountLx17Res
    {
        public class Records
        {
            public string E_IVNUM { get; set; }
            public string E_LGNUM { get; set; }
            public string E_NVERS { get; set; }
        }

        public Records Record { get; set; }
    }
}
