using EAMS.Helper;
using EAMS_ACore.AuthInterfaces;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EAMS_BLL.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly IEamsService _EAMSService;
        private readonly IEamsRepository _eamsRepository;
        private readonly INotificationService _notificationService;
        private readonly UserManager<UserRegistration> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository, UserManager<UserRegistration> userManager, RoleManager<IdentityRole> roleManager, IEamsService eamsService, IEamsRepository eamsRepository,INotificationService notificationService)
        {
            _configuration = configuration;
            _authRepository = authRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _EAMSService = eamsService;
            _eamsRepository = eamsRepository;
            _notificationService = notificationService;
        }

        #region AddDynamicRole && Get Role
        public async Task<ServiceResponse> AddDynamicRole(Role role)
        {

            return await _authRepository.AddDynamicRole(role);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _authRepository.GetRoles();
        }

        public async Task<List<UserRegistration>> GetUsersByRoleId(string roleId)
        {
            return await _authRepository.GetUsersByRoleId(roleId);
        }
        #endregion

        #region Login && Generate Token
        public async Task<Token> LoginAsync(Login login)
        {
            Token _Token = new();

            // Check if the user exists
            var user = await _authRepository.CheckUserLogin(login);
            if (user is null)
            {
                // Return an appropriate response when the user is not found
                return new Token()
                {
                    IsSucceed = false,
                    Message = "User Name or Password is Invalid"
                };
            }
            else
            {
                var userProfile = await _authRepository.GetDashboardProfile(user.Id);

                // Retrieve user roles
                var userRoles = await _authRepository.GetRoleByUser(login);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId",user.Id),
                    new Claim("StateMasterId",userProfile.StateMasterId.ToString()),
                    new Claim("DistrictMasterId",userProfile.DistrictMasterId.ToString()),
                    new Claim("PCMasterId",userProfile.PCMasterId.ToString()),
                    new Claim("AssemblyMasterId", userProfile.DistrictAssemblyMasterId == 0 ? userProfile.PCAssemblyMasterId.ToString() : userProfile.DistrictAssemblyMasterId.ToString()),

                };

                // Add user roles to authClaims
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
                }

                // Generate tokens
                _Token.AccessToken = GenerateToken(authClaims);
                _Token.RefreshToken = GenerateRefreshToken();
                _Token.Message = "Success";

                // Update user details with tokens
                if (user != null)
                {

                    var expireRefreshToken = BharatTimeDynamic(0, 7, 0, 0, 0);

                    var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration["JWTKey:RefreshTokenValidityInDays"]);
                    user.RefreshToken = _Token.RefreshToken;
                    user.RefreshTokenExpiryTime = expireRefreshToken;

                    // Update user and handle any exceptions
                    try
                    {
                        var updateUserResult = await _authRepository.UpdateUser(user);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it appropriately
                        // You may also want to return an error response
                        return new Token()
                        {
                            IsSucceed = false,
                            Message = "Error updating user: " + ex.Message
                        };
                    }
                }

                // Return the generated token
                return new Token()
                {
                    IsSucceed = true,
                    Message = "Success",
                    AccessToken = _Token.AccessToken,
                    RefreshToken = _Token.RefreshToken,
                };
            }
        }


        #endregion

        #region Register
        public async Task<ServiceResponse> RegisterAsync(UserRegistration userRegistration, List<string> roleIds)
        {
            var userExists = await _authRepository.FindUserByName(userRegistration);
            if (userExists.IsSucceed == false)
            {
                return userExists;

            }
            else
            {
                

                var createUserResult = await _authRepository.CreateUser(userRegistration, roleIds);

                if (createUserResult.IsSucceed == true)
                {
                    return createUserResult;

                }
                else
                {
                    return createUserResult;
                }
            }
            throw new NotImplementedException();

        }
        #endregion

        #region ValidateMobile && Generate OTP 
        public async Task<Response> ValidateMobile(ValidateMobile validateMobile)
        {
            Response response = new();
            var soRecord = await _authRepository.ValidateMobile(validateMobile);


            if (soRecord != null)
            {
                if (soRecord.IsLocked == false)
                {
                    if (validateMobile.Otp.Length >= 5)
                    {
                        var isOtpSame = soRecord.OTP == validateMobile.Otp;
                        if (isOtpSame == true)
                        {
                            var timeNow = BharatDateTime();
                            // Check if OTP is still valid
                            if (timeNow <= soRecord.OTPExpireTime)
                            {
                                var userAssembly = await _eamsRepository.GetAssemblyByCode(soRecord.SoAssemblyCode.ToString());

                                var authClaims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name,soRecord.SoName),
                                    new Claim(ClaimTypes.MobilePhone,soRecord.SoMobile),
                                    new Claim("StateMasterId",userAssembly.StateMasterId.ToString()),
                                    new Claim("DistrictMasterId",userAssembly.DistrictMasterId.ToString()),
                                    new Claim("AssemblyMasterId",userAssembly.AssemblyMasterId.ToString()),
                                    new Claim("SoId",soRecord.SOMasterId.ToString()),
                                    new Claim("JWTID", Guid.NewGuid().ToString()),
                                    new Claim(ClaimTypes.Role,"SO")
                                };
                                // Generate tokens
                                response.AccessToken = GenerateToken(authClaims);
                                response.RefreshToken = GenerateRefreshToken();
                                response.Message = "OTP Verified Successfully ";
                                response.Status = RequestStatusEnum.OK;
                                var expireRefreshToken = BharatTimeDynamic(0, 7, 0, 0, 0); ;


                                soRecord.RefreshToken = response.RefreshToken;
                                soRecord.RefreshTokenExpiryTime = expireRefreshToken;
                                var isSucceed = await _authRepository.SectorOfficerMasterRecord(soRecord);
                                if (isSucceed.IsSucceed == true)
                                {
                                    return response;
                                }
                                else
                                {
                                    return new Response()
                                    {
                                        Status = RequestStatusEnum.BadRequest
                                    };
                                }
                            }
                            else
                            {
                                return new Response()
                                {
                                    Status = RequestStatusEnum.BadRequest,
                                    Message = "OTP Expired"
                                };
                            }

                        }
                        else if (isOtpSame == false)
                        {

                            return new Response()
                            {
                                Status = RequestStatusEnum.BadRequest,
                                Message = "OTP Invalid"

                            };

                        }
                    }
                    else
                    {
                        var otp = GenerateOTP();
                       
                        SectorOfficerMaster sectorOfficerMaster = new SectorOfficerMaster()
                        {
                            SoMobile = validateMobile.MobileNumber,
                            OTP = otp,
                            OTPAttempts = 1,
                            OTPGeneratedTime = BharatDateTime(),
                            OTPExpireTime = BharatTimeDynamic(0, 0, 0, 0, 60)
                        };

                        var isSucceed = await _authRepository.SectorOfficerMasterRecord(sectorOfficerMaster);
                        if (isSucceed.IsSucceed == true)
                        {
                            var isSucced = await _notificationService.SendOtp(sectorOfficerMaster.SoMobile.ToString(),otp);
                            return new Response()
                            {
                                Status = RequestStatusEnum.OK,
                                //Message = "OTP Sent Successfully " + otp +"/"+"Response: "+ isSucced.Message,
                                Message = "OTP Sent Successfully " + otp ,

                            };
                        }
                    }




                }
                else
                {
                    return new Response
                    {
                        Status = RequestStatusEnum.BadRequest,
                        Message = "Your Account is Locked"
                    };
                }
            }
            else
            {
                return new Response()
                {
                    Status = RequestStatusEnum.NotFound,
                    Message = "Mobile Number does not exist"
                };

            }

            return new Response()
            {
                Status = RequestStatusEnum.BadRequest,
                Message = "Unexpected error occurred"
            };

        }
        public static string GenerateOTP(int length = 6)
        {
            const string chars = "123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        #endregion

        #region Common DateTime Methods
        private DateTime? BharatDateTime()
        {
            DateTime dateTime = DateTime.Now;
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            TimeSpan istOffset = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(30);
            TimeZoneInfo istTimeZone = TimeZoneInfo.CreateCustomTimeZone("IST", istOffset, "IST", "IST");
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);

            return DateTime.SpecifyKind(hiINDateTime, DateTimeKind.Utc);
        }


        /// <summary>
        /// if developer want UTC Kind Time only for month just pass month and rest fill 00000
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private DateTime BharatTimeDynamic(int month, int day, int hour, int minutes, int seconds)
        {
            DateTime dateTime = DateTime.Now;
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            TimeSpan istOffset = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(30);
            TimeZoneInfo istTimeZone = TimeZoneInfo.CreateCustomTimeZone("IST", istOffset, "IST", "IST");
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);

            if (month != 0)
            {
                hiINDateTime = DateTime.SpecifyKind(hiINDateTime.AddMonths(month), DateTimeKind.Utc);

            }
            else if (day != 0)
            {
                hiINDateTime = DateTime.SpecifyKind(hiINDateTime.AddDays(day), DateTimeKind.Utc);

            }
            else if (hour != 0)
            {
                hiINDateTime = DateTime.SpecifyKind(hiINDateTime.AddHours(hour), DateTimeKind.Utc);

            }
            else if (minutes != 0)
            {
                hiINDateTime = DateTime.SpecifyKind(hiINDateTime.AddMinutes(minutes), DateTimeKind.Utc);
            }
            else if (seconds != 0)
            {

                hiINDateTime = DateTime.SpecifyKind(hiINDateTime.AddSeconds(seconds), DateTimeKind.Utc);

            }

            return hiINDateTime;


        }

        #endregion

        #region GenerateToken && Refresh Token
        public async Task<Token> GetRefreshToken(GetRefreshToken model)
        {
            Token _TokenViewModel = new();
            var principal = await GetPrincipalFromExpiredToken(model.AccessToken);
            string username = principal.Identity.Name;
            var role = principal.Claims.FirstOrDefault(d => d.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;


            if (role is "SO")
            {
                var soId = principal.Claims.FirstOrDefault(d => d.Type == "SoId").Value;
                var soUser = await _authRepository.GetSOById(Convert.ToInt32(soId));
                if (soUser == null || !IsRefreshTokenValid(soUser.RefreshTokenExpiryTime))
                {
                    _TokenViewModel.IsSucceed = false;
                    _TokenViewModel.Message = "Invalid access token or refresh token";
                    return _TokenViewModel;
                }

                var authClaims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name,soUser.SoName),
                                    new Claim("SoId",soUser.SOMasterId.ToString()),
                                    new Claim("JWTID", Guid.NewGuid().ToString()),
                                    new Claim(ClaimTypes.Role,"SO")
                                };
                var newAccessToken = GenerateToken(authClaims);
                var newRefreshToken = GenerateRefreshToken();
                var expireRefreshToken = BharatTimeDynamic(0, 7, 0, 0, 0);
                soUser.RefreshToken = newRefreshToken;
                soUser.RefreshTokenExpiryTime = expireRefreshToken;
                var isSucceed = await _authRepository.SectorOfficerMasterRecord(soUser);
                if (isSucceed.IsSucceed == true)
                {
                    _TokenViewModel.IsSucceed = true;
                    _TokenViewModel.Message = "Success";
                    _TokenViewModel.AccessToken = newAccessToken;
                    _TokenViewModel.RefreshToken = newRefreshToken;
                }
                else
                {
                    _TokenViewModel.IsSucceed = false;
                    _TokenViewModel.Message = "Invalid access token or refresh token";
                }
            }
            else
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    _TokenViewModel.IsSucceed = false;
                    _TokenViewModel.Message = "Invalid access token or refresh token";
                    return _TokenViewModel;
                }


                var authClaims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim("UserId",user.Id),
                   new Claim(ClaimTypes.Role,role)

                };
                var newAccessToken = GenerateToken(authClaims);
                var newRefreshToken = GenerateRefreshToken();
                var expireRefreshToken = BharatTimeDynamic(0, 7, 0, 0, 0);
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = (DateTime)expireRefreshToken;
                await _userManager.UpdateAsync(user);

                _TokenViewModel.IsSucceed = true;
                _TokenViewModel.Message = "Success";
                _TokenViewModel.AccessToken = newAccessToken;
                _TokenViewModel.RefreshToken = newRefreshToken;
            }
            return _TokenViewModel;
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //var expireAccessToken = BharatTimeDynamic(0, 0, 4, 0, 0);

            var expireAccessToken = DateTime.UtcNow.AddHours(4);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7082",
                Audience = "https://localhost:3000",
                Expires = expireAccessToken,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // Disable lifetime validation

                ValidAudience = "https://localhost:3000",
                ValidIssuer = "https://localhost:7082",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDFSADFdfafeitt32t2r457f4f8ewf4waefeafjewfweAEFSDAFFEWFWAEAFaffd")),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                // Log claims for debugging purposes
                var tokenClaims = jwtSecurityToken.Claims.Select(c => $"{c.Type}: {c.Value}");

                return principal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool IsRefreshTokenValid(DateTime refreshTokenExpiryTime)
        {
            // Implement your business logic to check whether the refresh token is still valid.
            // You might consider checking against the current date, or additional custom conditions.
            return refreshTokenExpiryTime > DateTime.Now;
        }
        #endregion

        #region CreateSO Pin
        public async Task<ServiceResponse> CreateSOPin(CreateSOPin createSOPin, string soID)
        {
            return await _authRepository.CreateSOPin(createSOPin, soID);
        }




        #endregion

        #region UserDetail
        public async Task<DashBoardProfile> GetDashboardProfile(string UserID)
        {
            return await _authRepository.GetDashboardProfile(UserID);
        }
        
        #endregion
    }
}
