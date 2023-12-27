using EAMS.Helper;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using EAMS_ACore.Models;
using EAMS_DAL.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

                var user = await _userManager.FindByNameAsync(userRegistration.UserName);
                user.UserStates = userRegistration.UserStates;
                user.UserDistricts = userRegistration.UserDistricts;
                user.UserAssemblies = userRegistration.UserAssemblies;
                user.UserPCConstituencies = userRegistration.UserPCConstituencies;

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

                 

                _context.SaveChanges();

                return new ServiceResponse()
                {
                    IsSucceed = true,
                    Message = $"User '{userRegistration.UserName}' created successfully!."
                };
            }
            catch (Exception ex)
            {
                // Log the exception details for investigation
                // Example: _logger.LogError(ex, "An error occurred while creating a user.");
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

        public async Task<UserList> GetDashboardProfile(string userId)
        {

            var userRecord = await _userManager.FindByIdAsync(userId);
            var stateId = await _context.UserState.Where(d => d.Id == userRecord.Id).FirstOrDefaultAsync();
            var districtId = await _context.UserDistrict.Where(d => d.Id == userRecord.Id).FirstOrDefaultAsync();
            var assemblyId = await _context.UserAssembly.Where(d => d.Id == userRecord.Id).FirstOrDefaultAsync();
            var pcId= await _context.UserPCConstituency.Where(d => d.Id == userRecord.Id).FirstOrDefaultAsync();

            var stateName = await _context.StateMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateId)).Select(d => d.StateName).FirstOrDefaultAsync();
            var districtName = await _context.DistrictMaster.Where(d => d.DistrictMasterId == Convert.ToInt32(districtId)).Select(d => d.DistrictName).FirstOrDefaultAsync();
            var assemblyName = await _context.AssemblyMaster.Where(d => d.AssemblyMasterId == Convert.ToInt32(assemblyId)).Select(d => d.AssemblyName).FirstOrDefaultAsync();
            var pcName = await _context.ParliamentConstituencyMaster.Where(d => d.PCMasterId == Convert.ToInt32(pcId)).Select(d => d.PcName).FirstOrDefaultAsync();
            if (userRecord != null)
            {
                var roles = await _userManager.GetRolesAsync(userRecord);

                var rolesList = roles.Select(role => new Role
                {

                    RoleId = role,
                    RoleName = role
                }).ToList();

                if (userRecord != null && rolesList != null)
                {
                    UserList userList = new UserList()
                    {
                        Name = userRecord.UserName,
                        MobileNumber = userRecord.PhoneNumber,
                        StateId = Convert.ToInt32(stateId),
                        StateName = stateName,
                        DistrictId = Convert.ToInt32(districtId),
                        DistrictName = districtName,
                        AssemblyId = Convert.ToInt32(assemblyId),
                        AssemblyName = assemblyName,
                        Roles= rolesList

                    };

                    return userList;


                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }


        }

    }
}