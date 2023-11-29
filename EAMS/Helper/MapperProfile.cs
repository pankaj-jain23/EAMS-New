using AutoMapper;
using EAMS.AuthViewModels;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;

namespace EAMS.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region LoginViewModel Login  
            CreateMap<LoginViewModel, Login>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)) 
                .ReverseMap();
            #endregion

            #region UserRegistration UserRegistration  
            CreateMap<UserRegistrationViewModel, UserRegistration>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
                .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictMasterId))
                .ForMember(dest => dest.AssemblyMasterId, opt => opt.MapFrom(src => src.AssemblyMasterId))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
            #endregion

            #region StateMasterViewModel and  StateMaster 
            CreateMap<StateMasterViewModel, StateMaster>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.StateStatus, opt => opt.MapFrom(src => src.IsStatus))
                .ReverseMap();

            #endregion

            #region AddStateMasterViewModel and  StateMaster 
            CreateMap<AddStateMasterViewModel, StateMaster>() 
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
                .ForMember(dest => dest.StateStatus, opt => opt.MapFrom(src => src.IsStatus))
                .ReverseMap();

            #endregion

            #region CombinedMaster and DistrictMasterViewModel  

            CreateMap<CombinedMaster, DistrictMasterViewModel>()
               .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateId))
               .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictId))
               .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
               .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.DistrictName))
               .ForMember(dest => dest.DistrictCode, opt => opt.MapFrom(src => src.DistrictCode))
               .ReverseMap();

            #endregion

            #region AssemblyMasterViewModel AssemblyMaster
            CreateMap<AssemblyMasterViewModel, AssemblyMaster>()
                .ReverseMap();
            #endregion

            #region DistrictMasterViewModel and DistrictMaster 
            CreateMap<DistrictMasterViewModel, DistrictMaster>()
             .ReverseMap();

            #endregion

            #region AddDistrictMasterViewModel and DistrictMaster 
            CreateMap<AddDistrictMasterViewModel, DistrictMaster>()
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
                .ForMember(dest => dest.BoothStatus, opt => opt.MapFrom(src => src.IsStatus))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.TotalVoters, opt => opt.MapFrom(src => src.TotalVoters))

             .ReverseMap();


            #endregion

            #region BoothMappingViewModel BoothMaster

            CreateMap<BoothMappingViewModel, BoothMaster>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
                .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictMasterId))
                .ForMember(dest => dest.AssemblyMasterId, opt => opt.MapFrom(src => src.AssemblyMasterId))
                .ForMember(dest => dest.BoothMasterId, opt => opt.MapFrom(src => src.BoothMasterId))
                 .ForMember(dest => dest.AssignedBy, opt => opt.MapFrom(src => src.AssignedBy))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo))
                .ForMember(dest => dest.IsAssigned, opt => opt.MapFrom(src => src.IsAssigned))


             .ReverseMap();
            #endregion

            #region BoothReleaseViewModel BoothMaster

            CreateMap<BoothReleaseViewModel, BoothMaster>()
                .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
                .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictMasterId))
                .ForMember(dest => dest.AssemblyMasterId, opt => opt.MapFrom(src => src.AssemblyMasterId))
                .ForMember(dest => dest.BoothMasterId, opt => opt.MapFrom(src => src.BoothMasterId))
                .ForMember(dest => dest.IsAssigned, opt => opt.MapFrom(src => src.IsAssigned))


             .ReverseMap();
            #endregion

            #region EventMasterViewModel and EventMaster

            CreateMap<EventMasterViewModel, EventMaster>()
                .ForMember(dest => dest.EventMasterId, opt => opt.MapFrom(src => src.EventMasterId))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dest => dest.EventSequence, opt => opt.MapFrom(src => src.EventSequence))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsStatus))
                .ReverseMap();

            #endregion

            #region SectorOfficerViewModel SectorOfficerMaster
            CreateMap<SectorOfficerViewModel, SectorOfficerMaster>()
              .ForMember(dest => dest.SOMasterId, opt => opt.MapFrom(src => src.SoId))
              .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
              .ForMember(dest => dest.SoOfficeName, opt => opt.MapFrom(src => src.SoOfficeName))
              .ForMember(dest => dest.SoName, opt => opt.MapFrom(src => src.SoName))
              .ForMember(dest => dest.SoMobile, opt => opt.MapFrom(src => src.SoMobile))
              .ForMember(dest => dest.SoDesignation, opt => opt.MapFrom(src => src.SoDesignation))
              .ForMember(dest => dest.SoAssemblyCode, opt => opt.MapFrom(src => src.SoAssemblyCode))
              .ReverseMap();
            #endregion

            #region AddSectorOfficerViewModel SectorOfficerMaster
            CreateMap<AddSectorOfficerViewModel, SectorOfficerMaster>() 
              .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
              .ForMember(dest => dest.SoOfficeName, opt => opt.MapFrom(src => src.SoOfficeName))
              .ForMember(dest => dest.SoName, opt => opt.MapFrom(src => src.SoName))
              .ForMember(dest => dest.SoMobile, opt => opt.MapFrom(src => src.SoMobile))
              .ForMember(dest => dest.SoDesignation, opt => opt.MapFrom(src => src.SoDesignation))
              .ForMember(dest => dest.SoAssemblyCode, opt => opt.MapFrom(src => src.SoAssemblyCode))
              .ReverseMap();
            #endregion

# region SectorOfficerBoothViewModel CombinedMaster
            CreateMap<SectorOfficerBoothViewModel, CombinedMaster>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateMasterId))
                .ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.DistrictMasterId))
                .ForMember(dest => dest.AssemblyId, opt => opt.MapFrom(src => src.AssemblyMasterId))
                  .ForMember(dest => dest.soMasterId, opt => opt.MapFrom(src => src.SoId))
                 .ForMember(dest => dest.BoothName, opt => opt.MapFrom(src => src.BoothName))
                .ForMember(dest => dest.BoothMasterId, opt => opt.MapFrom(src => src.BoothMasterId))
                .ForMember(dest => dest.IsAssigned, opt => opt.MapFrom(src => src.IsAssigned))


             .ReverseMap();
            #endregion
            #region PCMaster
            CreateMap<PCViewModel, ParliamentConstituencyMaster>()
                .ReverseMap();
            #endregion

            #region Event Activity

            CreateMap<ElectionInfoViewModel, ElectionInfoMaster>()
         .ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId)) 
         .ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictMasterId)) 
         .ForMember(dest => dest.AssemblyMasterId, opt => opt.MapFrom(src => src.AssemblyMasterId)) 
         .ForMember(dest => dest.BoothMasterId, opt => opt.MapFrom(src => src.BoothMasterId))  
         .ForMember(dest => dest.IsPartyDispatched, opt => opt.MapFrom(src => src.EventStatus))  
         .ForMember(dest => dest.IsPartyReached, opt => opt.MapFrom(src => src.EventStatus))  


         .ReverseMap();
            #endregion

        }
    }
}
