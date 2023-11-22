using AutoMapper;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.HelperModels;

namespace EAMS.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // CreateMap should be called directly on the MapperConfigurationExpression
            CreateMap<StateMasterViewModel, StateMaster>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.StateStatus, opt => opt.MapFrom(src => src.StateStatus))
                .ReverseMap();

            CreateMap<CombinedMaster, DistrictMasterViewModel > ()
               .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateId)) 
               .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictId)) 
               .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName)) 
               .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.DistrictName)) 
               .ForMember(dest => dest.DistrictCode, opt => opt.MapFrom(src => src.DistrictCode)) 
               .ReverseMap();

            CreateMap<DistrictMasterViewModel, DistrictMaster>() 
             .ReverseMap();
        }
    }
}
