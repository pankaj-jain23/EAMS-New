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
using System.Text;

namespace EAMS_BLL.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            _authRepository = authRepository;
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
            throw new NotImplementedException();
        }
        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        #endregion

        #region Register
        public Task<AuthServiceResponse> RegisterAsync(UserRegistration userRegistration)
        {
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

       
    }
}
