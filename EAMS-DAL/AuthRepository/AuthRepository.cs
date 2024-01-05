using EAMS_ACore;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using EAMS_DAL.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace EAMS_DAL.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<UserRegistration> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly EamsContext _context;



        public AuthRepository(UserManager<UserRegistration> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, EamsContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;

        }
        #region AddDynamicRole && Get Role
        public async Task<ServiceResponse> AddDynamicRole(Role role)
        {
            var existingRole = await _roleManager.FindByNameAsync(role.RoleName);
            if (existingRole != null)
            {
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "Role already exists!"
                };
            }

            // Create a new role
            var newRole = new IdentityRole(role.RoleName);

            // Add the role to the database
            var result = await _roleManager.CreateAsync(newRole);

            if (!result.Succeeded)
            {
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "Role creation failed! Please check role details and try again."
                };
            }
            else
            {
                return new ServiceResponse()
                {
                    IsSucceed = true,
                    Message = "Role created successfully!"
                };

            }
        }

        public async Task<List<Role>> GetRoles()
        {
            var identityRoles = await _roleManager.Roles.ToListAsync();
            var roles = identityRoles.Select(identityRole => new Role
            {
                RoleId = identityRole.Id,
                RoleName = identityRole.Name
            }).ToList();
            return roles;
        }
        #endregion

        public async Task<List<UserRegistration>> GetUsersByRoleId(string roleId)
        {
            var identityRoles = await _roleManager.FindByIdAsync(roleId);
            var userInRTole = await _userManager.GetUsersInRoleAsync(identityRoles.Name);

            //GetDashboardProfile(userInRTole.)


            return userInRTole.ToList();
        }

        #region LoginAsync && GenerateToken
        public async Task<ServiceResponse> LoginAsync(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            if (user is null)
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials"
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!isPasswordCorrect)
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials"
                };

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //  var token = GenerateNewJsonWebToken(authClaims);

            return new ServiceResponse()
            {
                IsSucceed = true,
                // Message = token
            };
        }
        #endregion

        #region RegisterAsync

        public async Task<ServiceResponse> RegisterAsync(UserRegistration userRegistration)
        {
            throw new NotImplementedException();
        }





        #endregion

        #region FindUserByName
        public async Task<ServiceResponse> FindUserByName(UserRegistration userRegistration)
        {
            var userExists = await _userManager.FindByNameAsync(userRegistration.UserName);
            if (userExists != null)
            {
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "User Already Exist"
                };
            }
            else
            {
                return new ServiceResponse()
                {
                    IsSucceed = true,
                    Message = "User Not Exist"
                };

            }
        }
        #endregion


        public async Task<List<UserRegistration>> FindUserListByName(string userName)
        {
            var users = await _userManager.Users
                .Where(u => EF.Functions.Like(u.UserName.ToUpper(), "%" + userName.ToUpper() + "%"))
                .ToListAsync();

            return users;
        }


        #region Check User Login
        public async Task<UserRegistration> CheckUserLogin(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            // Use PasswordHasher to verify the password
            var passwordVerificationResult = await _userManager.CheckPasswordAsync(user, login.Password);

            if (passwordVerificationResult == true)
            {
                // Password is correct 
                return user;
            }
            else
            {
                // Password is incorrect
                return null;
            }
        }
        #endregion

        #region GetRoleByUser
        public async Task<List<Role>> GetRoleByUser(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            if (user == null)
            {
                // Handle the case where the user is not found
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var rolesList = roles.Select(role => new Role
            {
                RoleId = role,
                RoleName = role
            }).ToList();

            return rolesList;
        }

        #endregion

        #region CreateUser
        public async Task<ServiceResponse> CreateUser(UserRegistration userRegistration, List<string> roleIds)
        {
            try
            {
                var isExist = await _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
                if (isExist.Count > 0)
                {
                    foreach (var userState in userRegistration.UserStates)
                    {
                        if (isExist.Any(d => d.Name.Contains("ECI") || d.Name.Contains("SuperAdmin") || d.Name.Contains("StateAdmin")))
                        {
                            var districtList = _context.DistrictMaster.OrderBy(d => d.DistrictMasterId)
                                                    .Where(d => d.StateMasterId == userState.StateMasterId)
                                                    .Select(d => new UserDistrict
                                                    {
                                                        DistrictMasterId = d.DistrictMasterId,
                                                    })
                                                    .ToList();
                            var pcList = _context.ParliamentConstituencyMaster.OrderBy(d => d.PCMasterId)
                                                      .Where(d => d.StateMasterId == userState.StateMasterId)
                                                      .Select(d => new UserPCConstituency
                                                      {
                                                          PCMasterId = d.PCMasterId,
                                                      })
                                                      .ToList();

                            var matchingUserState = userRegistration.UserStates.FirstOrDefault(us => us.StateMasterId == userState.StateMasterId);

                            if (matchingUserState != null)
                            {
                                matchingUserState.UserDistrict = districtList;
                                matchingUserState.UserPCConstituency = pcList;
                            }

                        }

                        if (isExist.Any(d => d.Name.Contains("DistrictAdmin")))
                        {
                            foreach (var district in userState.UserDistrict)
                            {
                                var assemblieList = _context.AssemblyMaster.OrderBy(d => d.AssemblyMasterId)
                                    .Where(d => d.StateMasterId == userState.StateMasterId || d.DistrictMasterId == district.DistrictMasterId)
                                    .Select(d => new UserAssembly
                                    {
                                        AssemblyMasterId = d.AssemblyMasterId,
                                        // Set other properties as needed
                                    })
                                    .ToList();

                                var matchingUserState = userRegistration.UserStates.FirstOrDefault(us => us.StateMasterId == userState.StateMasterId);

                                if (matchingUserState != null)
                                {
                                    foreach (var assemblie in matchingUserState.UserDistrict)
                                    {
                                        // Create a list of UserAssembly from the list of AssemblyMaster
                                        var userAssemblyList = assemblieList.Select(assembly => new UserAssembly
                                        {
                                            AssemblyMasterId = assembly.AssemblyMasterId,
                                            // Set other properties as needed
                                        }).ToList();

                                        // Assign the list of UserAssembly to the UserAssembly property
                                        assemblie.UserAssembly = userAssemblyList;
                                    }
                                }
                            }
                        }

                        if (isExist.Any(d => d.Name.Contains("PC")))
                        {
                            foreach (var pc in userState.UserPCConstituency)
                            {
                                var assemblieList = _context.AssemblyMaster.OrderBy(d => d.AssemblyMasterId)
                                    .Where(d => d.StateMasterId == userState.StateMasterId || d.PCMasterId == pc.PCMasterId)
                                    .Select(d => new UserAssembly
                                    {
                                        AssemblyMasterId = d.AssemblyMasterId,
                                        // Set other properties as needed
                                    })
                                    .ToList();

                                var matchingUserState = userRegistration.UserStates.FirstOrDefault(us => us.StateMasterId == userState.StateMasterId);

                                if (matchingUserState != null)
                                {
                                    foreach (var assemblie in matchingUserState.UserPCConstituency)
                                    {
                                        // Create a list of UserAssembly from the list of AssemblyMaster
                                        var userAssemblyList = assemblieList.Select(assembly => new UserAssembly
                                        {
                                            AssemblyMasterId = assembly.AssemblyMasterId,
                                            // Set other properties as needed
                                        }).ToList();

                                        // Assign the list of UserAssembly to the UserAssembly property
                                        assemblie.UserAssembly = userAssemblyList;
                                    }
                                }
                            }
                        }

                        if (isExist.Any(d => d.Name.Contains("ARO")))
                        {

                            foreach (var district in userState.UserDistrict)
                            {
                                if (district.DistrictMasterId == 0)
                                {
                                    district.UserAssembly = null;
                                    userState.UserDistrict = null;
                                }
                               
                            }
                            foreach (var district in userState.UserPCConstituency)
                            {
                                if (district.PCMasterId == 0)
                                {
                                    district.UserAssembly = null;
                                    userState.UserPCConstituency = null;
                                }
                            }

                        }

                    }


                    var createUserResult = await _userManager.CreateAsync(userRegistration, userRegistration.PasswordHash);
                    if (!createUserResult.Succeeded)
                    {
                        return new ServiceResponse()
                        {
                            IsSucceed = false,
                            Message = "User creation failed! Please check user details and try again.",
                            // Log the error details for investigation
                        };
                    }
                }
                else
                {
                    return new ServiceResponse()
                    {
                        IsSucceed = false,
                        Message = $"Failed to assign roles to user '{userRegistration.UserName}'.",
                        // Log the error details for investigation
                    };

                }
                var user = await _userManager.FindByNameAsync(userRegistration.UserName);


                if (roleIds != null && roleIds.Any())
                {
                    var roles = await _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();

                    foreach (var role in roles)
                    {
                        var userRoleResult = await _userManager.AddToRoleAsync(user, role.Name);

                        if (!userRoleResult.Succeeded)
                        {
                            // Handle role assignment failure
                            return new ServiceResponse()
                            {
                                IsSucceed = false,
                                Message = $"Failed to assign roles to user '{userRegistration.UserName}'.",
                                // Log the error details for investigation
                            };
                        }
                    }

                }


                return new ServiceResponse()
                {
                    IsSucceed = true,
                    Message = $"User '{userRegistration.UserName}' created successfully!."
                };
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details for investigation
                foreach (var entry in ex.Entries)
                {
                    Console.WriteLine($"Entity Type: {entry.Entity.GetType().Name}");
                    Console.WriteLine($"Entity State: {entry.State}");
                    Console.WriteLine($"Original Values: {string.Join(", ", entry.OriginalValues.Properties.Select(p => $"{p.Name}: {entry.OriginalValues[p]}"))}");
                    Console.WriteLine($"Current Values: {string.Join(", ", entry.CurrentValues.Properties.Select(p => $"{p.Name}: {entry.CurrentValues[p]}"))}");

                    // Log entry details, such as entry.State, entry.OriginalValues, entry.CurrentValues, etc.
                }

                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                };
            }
        }

        #endregion

        #region  UpdateUser
        public async Task<ServiceResponse> UpdateUser(UserRegistration userRegistration)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
                DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var expireRefreshToken = hiINDateTime.AddDays(1);

                // Set DateTimeKind to Utc explicitly
                expireRefreshToken = DateTime.SpecifyKind(expireRefreshToken, DateTimeKind.Utc);

                userRegistration.RefreshTokenExpiryTime = expireRefreshToken;
                var updateUser = await _userManager.UpdateAsync(userRegistration);
                if (updateUser.Succeeded is true)
                {
                    return new ServiceResponse()
                    {
                        IsSucceed = true,
                        Message = "User Updated Succesfully"
                    };
                }
                else
                {
                    return new ServiceResponse()
                    {
                        IsSucceed = false,
                        Message = "User Updation Failed!!w"
                    };

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region ValidateMobile && Sector Officer Master
        public async Task<SectorOfficerMaster> ValidateMobile(ValidateMobile validateMobile)
        {
            var soRecord = await _context.SectorOfficerMaster.Where(d => d.SoMobile == validateMobile.MobileNumber).FirstOrDefaultAsync();
            return soRecord;
        }
        public async Task<ServiceResponse> SectorOfficerMasterRecord(SectorOfficerMaster sectorOfficerMaster)
        {
            var soRecord = _context.SectorOfficerMaster.Where(d => d.SoMobile == sectorOfficerMaster.SoMobile).FirstOrDefault();
            if (soRecord != null)
            {
                if (soRecord.IsLocked == false)
                {
                    soRecord.SoMobile = sectorOfficerMaster.SoMobile;
                    soRecord.IsLocked = false;
                    soRecord.OTP = sectorOfficerMaster.OTP;
                    soRecord.OTPGeneratedTime = DateTime.SpecifyKind(sectorOfficerMaster.OTPGeneratedTime ?? DateTime.UtcNow, DateTimeKind.Utc);
                    soRecord.OTPExpireTime = DateTime.SpecifyKind(sectorOfficerMaster.OTPExpireTime ?? DateTime.UtcNow, DateTimeKind.Utc);
                    soRecord.OTPAttempts = sectorOfficerMaster.OTPAttempts;
                    soRecord.RefreshToken = sectorOfficerMaster.RefreshToken;
                    soRecord.RefreshTokenExpiryTime = sectorOfficerMaster.RefreshTokenExpiryTime;
                }
                else if (sectorOfficerMaster.IsLocked == true)
                {
                    soRecord.IsLocked = true;

                }
                _context.SectorOfficerMaster.Update(soRecord);
                _context.SaveChanges();
                return new ServiceResponse
                {
                    IsSucceed = true,
                };
            }
            else
            {
                return new ServiceResponse
                {
                    IsSucceed = false,
                };
            }

        }


        #endregion

        #region CreateSO Pin
        public async Task<ServiceResponse> CreateSOPin(CreateSOPin createSOPin, string soId)
        {
            var soRecord = _context.SectorOfficerMaster.Where(d => d.SOMasterId == Convert.ToInt32(soId)).FirstOrDefault();
            if (soRecord == null)
            {
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "SO User Not Exist"
                };
            }
            else
            {
                soRecord.AppPin = createSOPin.ConfirmSOPin;
                _context.SectorOfficerMaster.Update(soRecord);
                _context.SaveChanges();
                return new ServiceResponse()
                {
                    IsSucceed = true,
                    Message = "PIN Created SuccessFully"
                };

            }
        }

        #endregion

        #region GetSOByNumber
        public async Task<SectorOfficerMaster> GetSOById(int soId)
        {
            var soRecord = _context.SectorOfficerMaster.Where(d => d.SOMasterId == soId).FirstOrDefault();
            if (soRecord is not null)
            {
                return soRecord;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region UserDetail
        public async Task<DashBoardProfile> GetDashboardProfile(string userId)
        {
            var userRecord = await _userManager.FindByIdAsync(userId);

            if (userRecord != null)
            {
                var userSubDetails = await _context.UserState
                    .Include(u => u.UserDistrict)
                        .ThenInclude(d => d.UserAssembly)
                    .Include(u => u.UserPCConstituency)
                        .ThenInclude(pc => pc.UserAssembly)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var roles = await _userManager.GetRolesAsync(userRecord);
                var rolesList = roles.ToList();

                var userDistrictDetails = userSubDetails?.UserDistrict?.ToList() ?? new List<UserDistrict>();
                var userDistrictAssemblyDetails = userDistrictDetails.SelectMany(d => d.UserAssembly).Distinct().ToList();
                var pcDetails = userSubDetails?.UserPCConstituency?.ToList() ?? new List<UserPCConstituency>();
                var userPCAssemblyDetails = pcDetails.SelectMany(d => d.UserAssembly).Distinct().ToList();
                var stateName = _context.StateMaster.FirstOrDefault(d => d.StateMasterId == userSubDetails.StateMasterId);
                var district = userDistrictDetails?.Any() == true ? _context.DistrictMaster.FirstOrDefault(d => d.DistrictMasterId == userDistrictDetails.Select(d => d.DistrictMasterId).FirstOrDefault()) : null;
                var assemblyDistrictId = userDistrictAssemblyDetails.Any() ? userDistrictAssemblyDetails.Select(d => d.AssemblyMasterId).FirstOrDefault() : (int?)null;
                var assemblyDistrict = assemblyDistrictId != null ? await GetAssemblyById(assemblyDistrictId.ToString()) : null;
                var pc = await GetPCList(stateName?.StateMasterId.ToString());
                var assemblyPcId = pc?.Select(d => d.PCMasterId).FirstOrDefault();
                var assemblyPc = assemblyPcId != null ? await GetAssemblyByPCId(stateName?.StateMasterId.ToString(), assemblyPcId.ToString()) : null;
                var stateCount = userRecord.UserStates?.Count() ?? 0;
                var districtMasterId = district?.DistrictMasterId ?? 0;
                var assemblyDistrictMasterId = assemblyDistrict?.AssemblyMasterId ?? 0;
                var pcMasterId = pc?.Select(d => d.PCMasterId).FirstOrDefault() ?? 0;
                var assemblyPcMasterId = assemblyPc?.Select(d => d.PCMasterId).FirstOrDefault() ?? 0;

                DashBoardProfile dashBoardProfile = new DashBoardProfile()
                {
                    Name = userRecord.UserName,
                    MobileNumber = userRecord.PhoneNumber,
                    UserEmail = userRecord.Email,
                    UserType = "DashBoard",
                    Roles = rolesList,
                    StateCount = stateCount,
                    StateName= stateName.StateName,
                    StateMasterId = stateCount > 0 ? userRecord.UserStates?.Select(d => d.StateMasterId).FirstOrDefault() ?? 0 : 0,
                    DistrictCount = districtMasterId > 0 ? userSubDetails?.UserDistrict?.Count() ?? 0 : 0,
                    DistrictName = district?.DistrictName ?? "0",
                    DistrictMasterId = districtMasterId,
                    DistrictAssemblyCount = assemblyDistrictMasterId > 0 ? userDistrictAssemblyDetails?.Count() ?? 0 : 0,
                    DistrictAssemblyMasterId = assemblyDistrictMasterId,
                    DistrictAssemblyName = assemblyDistrict?.AssemblyName ?? "0",
                    PCCount = pcMasterId > 0 ? userSubDetails?.UserPCConstituency?.Count() ?? 0 : 0,
                    PCMasterId = pcMasterId,
                    PCName = pc?.Select(d => d.PcName).FirstOrDefault() ?? "0",
                    PCAssemblyCount = assemblyPcMasterId > 0 ? userPCAssemblyDetails?.Count() ?? 0 : 0,
                    PCAssemblyMasterId = assemblyPcMasterId,
                    PCAssemblyName = assemblyPc?.Select(d => d.AssemblyName).FirstOrDefault() ?? "0",

                };

                return dashBoardProfile;
            }
            else
            {
                return null; // Return null when userRecord is null
            }
        }

        public async Task<List<AssemblyMaster>> GetAssemblyByDistrictId(string stateMasterId, string districtMasterId)
        {

            var asemData = await _context.AssemblyMaster
    .Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId))
    .OrderBy(d => d.PCMasterId)
    .Select(d => new AssemblyMaster
    {
        PCMasterId = d.PCMasterId,
        StateMasterId = d.StateMasterId,
        AssemblyCode = d.AssemblyCode,
        AssemblyName = d.AssemblyName,
        AssemblyType = d.AssemblyType,
        AssemblyStatus = d.AssemblyStatus,
        AssemblyCreatedAt = d.AssemblyCreatedAt
    })
    .ToListAsync();

            return asemData;
        }
        public async Task<List<ParliamentConstituencyMaster>> GetPCList(string stateMasterId)
        {

            var pcData = await _context.ParliamentConstituencyMaster
    .Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId))
    .OrderBy(d => d.PCMasterId)
    .Select(d => new ParliamentConstituencyMaster
    {
        PCMasterId = d.PCMasterId,
        StateMasterId = d.StateMasterId,
        PcCodeNo = d.PcCodeNo,
        PcName = d.PcName,
        PcType = d.PcType,
        PcStatus = d.PcStatus
    })
    .ToListAsync();

            return pcData;
        }
        public async Task<List<AssemblyMaster>> GetAssemblyByPCId(string stateMasterid, string PcMasterId)
        {

            var asemData = await _context.AssemblyMaster
    .Where(d => d.PCMasterId == Convert.ToInt32(PcMasterId) && d.StateMasterId == Convert.ToInt32(stateMasterid))
    .OrderBy(d => d.PCMasterId)
    .Select(d => new AssemblyMaster
    {
        PCMasterId = d.PCMasterId,
        StateMasterId = d.StateMasterId,
        AssemblyCode = d.AssemblyCode,
        AssemblyName = d.AssemblyName,
        AssemblyType = d.AssemblyType,
        AssemblyStatus = d.AssemblyStatus,
        AssemblyCreatedAt = d.AssemblyCreatedAt
    })
    .ToListAsync();

            return asemData;
        }
        public async Task<AssemblyMaster> GetAssemblyById(string assemblyMasterId)
        {
            var assemblyRecord = await _context.AssemblyMaster.Include(d => d.StateMaster).Include(d => d.DistrictMaster).Include(d => d.ParliamentConstituencyMaster).Where(d => d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId)).FirstOrDefaultAsync();
            return assemblyRecord;
        }
        #endregion


    }
}