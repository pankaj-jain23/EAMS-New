﻿using AutoMapper;
using EAMS.AuthViewModels;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
using EAMS_ACore.NotificationModels;

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
                //.ForMember(dest => dest.StateMasterId, opt => opt.MapFrom(src => src.StateMasterId))
                //.ForMember(dest => dest.DistrictMasterId, opt => opt.MapFrom(src => src.DistrictMasterId))
                //.ForMember(dest => dest.AssemblyMasterId, opt => opt.MapFrom(src => src.AssemblyMasterId))
                //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
            #endregion

            #region RoleViewModel Roles

            CreateMap<RolesViewModel, Role>()
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
            #endregion

            #region ValidateMobileViewModel ValidteMobile

            CreateMap<ValidateMobileViewModel, ValidateMobile>().ReverseMap();
            #endregion

            #region CreateSOPinViewModel CreateSOPin
            CreateMap<CreateSOPinViewModel, CreateSOPin>().ReverseMap();
            #endregion

            #region TokenViewModel Token
            CreateMap<TokenViewModel, Token>().ReverseMap();
            #endregion

            #region NotificationViewModel NotificationModel
            CreateMap<NotificationViewModel, Notification>().ReverseMap();
            #endregion

            #region GetRefreshTokenViewModel GetRefreshToken
            CreateMap<GetRefreshTokenViewModel, GetRefreshToken>().ReverseMap();
            #endregion

            #region UpdateMasterStatusViewModel UpdateMasterStatus

            CreateMap<UpdateMasterStatusViewModel, UpdateMasterStatus>().ReverseMap();
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

            #region UpdateEventStatusViewModel and EventMaster

            CreateMap<UpdateEventStatusViewModel, EventMaster>()
                .ForMember(dest => dest.EventMasterId, opt => opt.MapFrom(src => src.EventMasterId)) 
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

            #region SectorOfficerBoothViewModel CombinedMaster
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

            #region CombineMasterViewModel CombinedMaster
            CreateMap<CombinedMasterViewModel, CombinedMaster>()
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateMasterId))
                .ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.DistrictMasterId))
                .ForMember(dest => dest.AssemblyId, opt => opt.MapFrom(src => src.AssemblyMasterId))
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

            #region SlotManagement
            CreateMap<SlotTimeViewModel, SlotManagementMaster>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.EndTime) ? null : (TimeOnly?)TimeOnly.Parse(src.EndTime)))
            .ForMember(dest => dest.LockTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LockTime) ? null : (TimeOnly?)TimeOnly.Parse(src.LockTime)))
            .ForMember(dest => dest.SlotCreatedTime, opt => opt.MapFrom(src => DateTimeOffset.Now.ToUniversalTime()))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.Parse(src.StartDate))); // Add this line for StartDate mapping

            CreateMap<SlotManagementViewModel, List<SlotManagementMaster>>()
        .ConvertUsing((src, dest, context) =>
        {
            var slotManagements = src.slotTimes
                .Select(slotTime => context.Mapper.Map<SlotManagementMaster>(slotTime))
                .ToList();

            foreach (var slotManagement in slotManagements)
            {
                slotManagement.StateMasterId = src.StateMasterId;
                slotManagement.EventMasterId = src.EventMasterId;
            }

            return slotManagements;
        });
            #endregion



            #region InterruptionViewModel CombinedMaster
            CreateMap<InterruptionViewModel, PollInterruption>()
                .ForMember(dest => dest.BoothMasterId, opt => opt.MapFrom(src => src.boothMasterId))
                .ForMember(dest => dest.InterruptionType, opt => opt.MapFrom(src => src.Reason))

                .ForMember(dest => dest.StopTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.stopTime) ? null : (TimeOnly?)TimeOnly.Parse(src.stopTime)))
                 .ForMember(dest => dest.ResumeTime, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ResumeTime) ? null : (TimeOnly?)TimeOnly.Parse(src.ResumeTime)))
                 .ForMember(dest => dest.NewCU, opt => opt.MapFrom(src => src.newCU))
                .ForMember(dest => dest.NewBU, opt => opt.MapFrom(src => src.newBU))
                .ForMember(dest => dest.OldBU, opt => opt.MapFrom(src => src.oldBu))
                .ForMember(dest => dest.OldCU, opt => opt.MapFrom(src => src.oldCu))
             //.ForMember(dest => dest.IsPollInterrupted, opt => opt.MapFrom(src => src.IsPollInterrupted))
             //.ForMember(dest => dest.Flag, opt => opt.MapFrom(src => src.Flag))


             .ReverseMap();
            #endregion    

        }
    }
}
