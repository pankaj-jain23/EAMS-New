using EAMS.Helper;
using EAMS_ACore.AuthInterfaces;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
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
        private readonly UserManager<UserRegistration> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository, UserManager<UserRegistration> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _authRepository = authRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region AddDynamicRole && Get Role
        public async Task<AuthServiceResponse> AddDynamicRole(Role role)
        {

            return await _authRepository.AddDynamicRole(role);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _authRepository.GetRoles();
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
                // Retrieve user roles
                var userRoles = await _authRepository.GetRoleByUser(login);
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
                    DateTime dateTime = DateTime.Now;
                    DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
                    DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    var expireRefreshToken = hiINDateTime.AddDays(1);

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
                    Message="Success",
                    AccessToken=_Token.AccessToken,
                    RefreshToken=_Token.RefreshToken,
                };
            }
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            DateTime tokenExpireTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.AddHours(1), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: tokenExpireTime,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        #endregion

        #region Register
        public async Task<AuthServiceResponse> RegisterAsync(UserRegistration userRegistration, List<string> roleId)
        {
            var userExists = await _authRepository.FindUserByName(userRegistration);
            if (userExists.IsSucceed == false)
            {
                return userExists;

            }
            else
            {
                var createUserResult = await _authRepository.CreateUser(userRegistration, roleId);

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

        #region ValidateMobile && Generate OTP && OTP ExpireTime
        public async Task<Response> ValidateMobile(ValidateMobile validateMobile)
        {
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
                                var authClaims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name,"Test"),
                                    new Claim(ClaimTypes.NameIdentifier,"sd"),
                                    new Claim("JWTID", Guid.NewGuid().ToString()),
                                    new Claim("Roles","SO")
                                };
                                var token = GenerateNewJsonWebToken(authClaims);

                                return new Response()
                                {
                                    Status = RequestStatusEnum.OK,
                                    Message = "OTP Verified Successfully ",
                                    Token = token
                                };
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
                            OTPExpireTime = OTPExpireTime()
                        };

                        var isSucceed = await _authRepository.SectorOfficerMasterRecord(sectorOfficerMaster);
                        if (isSucceed.IsSucceed == true)
                        {
                            return new Response()
                            {
                                Status = RequestStatusEnum.OK,
                                Message = "OTP Sent Successfully " + otp,

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
                    Status = RequestStatusEnum.BadRequest,
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
            const string chars = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private DateTime? OTPExpireTime()
        {
            DateTime dateTime = DateTime.Now.AddSeconds(30);
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            return hiINDateTime;

        }
        #endregion

        private DateTime? BharatDateTime()
        {
            DateTime dateTime = DateTime.Now;
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            return hiINDateTime;
        }
      

        #region GenerateToken && Refresh Token
        public async Task<Token> GetRefreshToken(GetRefreshToken model)
        {
            Token _TokenViewModel = new();
            var principal =await GetPrincipalFromExpiredToken(model.AccessToken);
            string username = principal.Identity.Name;
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
            };
            var newAccessToken = GenerateToken(authClaims);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            _TokenViewModel.IsSucceed = true;
            _TokenViewModel.Message = "Success";
            _TokenViewModel.AccessToken = newAccessToken;
            _TokenViewModel.RefreshToken = newRefreshToken;
            return _TokenViewModel;
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            DateTime dateTime = DateTime.Now;
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            var expireAccessToken = hiINDateTime.AddHours(1);

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
            var te = _configuration["JWTKey:Secret"];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
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
                Console.WriteLine($"Token Claims: {string.Join(", ", tokenClaims)}");

                return principal;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        #endregion


    }
}
