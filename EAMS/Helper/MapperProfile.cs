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
            #region StateMasterViewModel and  StateMaster 
            CreateMap<StateMasterViewModel, StateMaster>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.StateStatus, opt => opt.MapFrom(src => src.StateStatus))
                .ReverseMap();

            #endregion

            #region CombinedMaster and DistrictMasterViewModel  

            CreateMap<CombinedMaster, DistrictMasterViewModel > ()
               .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateId)) 
               .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictId)) 
               .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName)) 
               .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.DistrictName)) 
               .ForMember(dest => dest.DistrictCode, opt => opt.MapFrom(src => src.DistrictCode)) 
               .ReverseMap();

            #endregion

            #region DistrictMasterViewModel and DistrictMaster 
            CreateMap<DistrictMasterViewModel, DistrictMaster>() 
             .ReverseMap();

            #endregion

            #region  BoothMasterViewModel and BoothMaster



            CreateMap<BoothMasterViewModel, BoothMaster>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
                .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictMasterId))
                .ForMember(dest => dest.AssemblyMasterId, opt => opt.MapFrom(src => src.AssemblyMasterId))
                .ForMember(dest => dest.BoothName, opt => opt.MapFrom(src => src.BoothName))
                .ForMember(dest => dest.BoothCode_No, opt => opt.MapFrom(src => src.BoothCode_No))
                .ForMember(dest => dest.BoothNoAuxy, opt => opt.MapFrom(src => src.BoothNoAuxy))
                .ForMember(dest => dest.BoothStatus, opt => opt.MapFrom(src => src.BoothStatus))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
               
             .ReverseMap();
            #endregion
        }
    }
}
