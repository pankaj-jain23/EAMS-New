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
        public async Task<AuthServiceResponse> LoginAsync(Login login)
        {
            Token _Token = new();  
            var userExists = await _authRepository.CheckUserLogin(login);

            //if (userExists.IsSucceed == false)
            //{  
            //    return _TokenViewModel;
            //}


            var userRoles = await _authRepository.GetRoleByUser(login);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, login.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
            }
            _Token.AccessToken = GenerateToken(authClaims);
            _Token.RefreshToken = GenerateRefreshToken();
            _Token.StatusCode = 1;
            _Token.StatusMessage = "Success";

            var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration["JWTKey:RefreshTokenValidityInDays"]);
           UserRegistration user=new UserRegistration();
            user.RefreshToken = _Token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_RefreshTokenValidityInDays);
            var updateUserResult=_authRepository.UpdateUser(user);


            return null;
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
            var userExists =await _authRepository.FindUserByName(userRegistration);
            if (userExists.IsSucceed==false)
            {
                return userExists;

            }
            else
            {
                var createUserResult = await _authRepository.CreateUser(userRegistration, roleId);

                if (createUserResult.IsSucceed==true)
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
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _TokenViewModel.StatusCode = 0;
                _TokenViewModel.StatusMessage = "Invalid access token or refresh token";
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

            _TokenViewModel.StatusCode = 1;
            _TokenViewModel.StatusMessage = "Success";
            _TokenViewModel.AccessToken = newAccessToken;
            _TokenViewModel.RefreshToken = newRefreshToken;
            return _TokenViewModel;
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                //Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                Expires = DateTime.UtcNow.AddMinutes(30),
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
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        #endregion


    }
}
