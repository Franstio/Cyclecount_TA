using AutoMapper;
using TA_PROJECT_CyclecountAPI.Model.API.Lx17;
using TA_PROJECT_CyclecountAPI.Model.LICC;
using TA_PROJECT_CyclecountAPI.Model.Lx17;

namespace TA_PROJECT_CyclecountAPI.Config
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<Lx17, Lx17Log>();
            CreateMap<LICC, Lx17>();
            CreateMap<Lx17, CountLx17Req.T_LINVs.T_LINV_Items>();
            CreateMap<CountLx17Req.T_LINVs.T_LINV_Items, Lx17>();
        }
    }
}
