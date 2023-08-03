using TA_PROJECT_CyclecountAPI.Model.LICC;

namespace TA_PROJECT_CyclecountAPI.Model.API.LICC
{
    public class LICCResponse
    {
        public class Item_ET_LICC
        {
            public Model.LICC.LICC[] item { get; set; } = new Model.LICC.LICC[0];
        }
        public Item_ET_LICC ET_LICC { get; set; } = new Item_ET_LICC();
        public LICCRequest.Item_S_ABCIN S_ABCIN { get; set; } = new LICCRequest.Item_S_ABCIN();
        public LICCRequest.Item_S_Matnr S_MATNR { get; set; } = new LICCRequest.Item_S_Matnr();
    }
}
