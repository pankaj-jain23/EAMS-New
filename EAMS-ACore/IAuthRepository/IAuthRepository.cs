﻿using EAMS.Helper;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.IAuthRepository
{
    public interface IAuthRepository
    {
        Task<ServiceResponse> RegisterAsync(UserRegistration userRegistration);
        Task<ServiceResponse> LoginAsync(Login login);

        Task<ServiceResponse> AddDynamicRole(Role role);
        Task<List<Role>> GetRoles();
        Task<List<UserRegistration>> GetUsersByRoleId(string roleId);
        Task<SectorOfficerMaster> ValidateMobile(ValidateMobile validateMobile);

        Task<ServiceResponse> SectorOfficerMasterRecord(SectorOfficerMaster sectorOfficerMaster);
        Task<ServiceResponse> FindUserByName(UserRegistration userRegistration);
        Task<List<UserRegistration>> FindUserListByName(string userName);
        Task<UserRegistration> CheckUserLogin(Login login);
        Task<ServiceResponse> CreateUser(UserRegistration userRegistration, List<string> roleIds);
        Task<ServiceResponse> UpdateUser(UserRegistration userRegistration);
        Task<List<Role>> GetRoleByUser(Login login);
        Task<ServiceResponse> CreateSOPin(CreateSOPin createSOPin, string soId);
        Task<SectorOfficerMaster> GetSOById(int soId);
        Task<DashBoardProfile> GetDashboardProfile(string userId);
        Task<List<AssemblyMaster>> GetAssemblyByDistrictId(string stateMasterid, string districtMasterId);

        Task<List<ParliamentConstituencyMaster>> GetPCList(string stateMasterId);

        Task<List<AssemblyMaster>> GetAssemblyByPCId(string stateMasterid, string PcMasterId);
        Task<AssemblyMaster> GetAssemblyById(string assemblyId);


    }
}
