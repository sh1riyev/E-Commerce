using System.Linq.Expressions;
using System.Web;
using AutoMapper;
using E_Commerce.Business.DTOs.AccountDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace E_Commerce.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISendEmail _sendEmail;
        private readonly UrlHelperService _urlHelper;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IDistributedCache _distributedCache;
        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, ISendEmail sendEmail, UrlHelperService urlHelper, ITokenService tokenService, IMapper mapper, IFileService fileService, IPhotoAccessor photoAccessor, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _sendEmail = sendEmail;
            _urlHelper = urlHelper;
            _tokenService = tokenService;
            _mapper = mapper;
            _fileService = fileService;
            _photoAccessor = photoAccessor;
            _distributedCache = distributedCache;
        }

        public async Task<ResponseObj> ForgetPassword(string email, string requestScheme, string requestHost)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null || appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "InValid user",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var urlHelper = _urlHelper.GetUrlHelper();
            string link = $"{requestScheme}://{requestHost}/ResetPassword/{HttpUtility.UrlEncode(appUser.Email)}/{HttpUtility.UrlEncode(token)}";
            string resetPasswordBody = string.Empty;
            using (StreamReader stream = new StreamReader("wwwroot/Verification/ResetPassword.html"))
            {
                resetPasswordBody = await stream.ReadToEndAsync();
            };
            resetPasswordBody = resetPasswordBody.Replace("{{link}}", link);
            resetPasswordBody = resetPasswordBody.Replace("{{userName}}", appUser.UserName);
            _sendEmail.Send("ilgarchsh@code.edu.az", "E-Commerce", appUser.Email, resetPasswordBody, "Reset Password");
            return new ResponseObj
            {
                ResponseMessage = $"reset password link sended to {appUser.UserName} ,\n Token : {token} ",
                StatusCode = (int)StatusCodes.Status200OK
            };
        }

        public async Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null)
        {
            return predicate == null ? false : await _userManager.Users.AnyAsync(predicate);
        }

        public async Task<ResponseObj> Login(UserLoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDto.EmailOrUserName);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(loginDto.EmailOrUserName);
                if (appUser == null)
                {
                    return new ResponseObj
                    {
                        ResponseMessage = "Invalid User",
                        StatusCode = (int)StatusCodes.Status400BadRequest
                    };
                }
            }
            if (appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "user is not exist",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            else if (!appUser.IsActive) return new ResponseObj
            {
                ResponseMessage = "User is not active",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            var resoult = await _signInManager.PasswordSignInAsync(appUser, loginDto.Password, true, true);
            if (resoult.IsLockedOut) return new ResponseObj
            {
                ResponseMessage = "User is locked",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            else if (!appUser.EmailConfirmed) return new ResponseObj
            {
                ResponseMessage = "email is not confirm",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            else if (!resoult.Succeeded) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = resoult.ToString()
            };
            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = _tokenService.CreateToken(appUser, roles)
            };
        }

        public async Task<ResponseObj> ProfileUpdate(string id, UserUpdateDto userUpdateDto, string requestScheme, string requestHost)
        {
            if (!await IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "User is not found"
            };
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (!appUser.EmailConfirmed) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "User email is not confirmed"
            };
            else if (await _userManager.IsLockedOutAsync(appUser)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "User is locked"
            };
            if (!appUser.IsSeller && userUpdateDto.IsSeller)
            {
                appUser.IsActive = false;
                await _userManager.AddToRoleAsync(appUser, "Seller");
                if (await _userManager.IsInRoleAsync(appUser, "User"))
                {
                    await _userManager.RemoveFromRoleAsync(appUser, "User");
                }
            }
            else if (appUser.IsSeller && !userUpdateDto.IsSeller)
            {
                await _userManager.RemoveFromRoleAsync(appUser, "Seller");
                if (!await _userManager.IsInRoleAsync(appUser, "User"))
                {
                    await _userManager.AddToRoleAsync(appUser, "User");
                }
            }
            if (appUser.Email != userUpdateDto.Email)
            {
                appUser.EmailConfirmed = false;
            }
            _mapper.Map(userUpdateDto, appUser);
            string oldPublicId = appUser.PublicId;
            if (userUpdateDto.ProfileImage != null)
            {
                if (!_fileService.IsImage(userUpdateDto.ProfileImage)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Upload Only Image"
                };
                else if (!_fileService.IsLengthSuit(userUpdateDto.ProfileImage, 1000)) return new ResponseObj
                {
                    ResponseMessage = "Image size must be smaller than 1kb",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
                var imageResoult = await _photoAccessor.AddPhoto(userUpdateDto.ProfileImage);
                appUser.ProfileImageUrl = imageResoult.SecureUrl.ToString();
                appUser.PublicId = imageResoult.PublicId;
            }
            appUser.UpdatedAt = DateTime.Now;
            IdentityResult resoult = await _userManager.UpdateAsync(appUser);
            if (!resoult.Succeeded)
            {
                if (userUpdateDto.ProfileImage != null)
                {
                    await _photoAccessor.DeletePhoto(appUser.PublicId);
                }
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(e => e.Description))
                };
            }
            if (oldPublicId != null)
            {
                await _photoAccessor.DeletePhoto(oldPublicId);
            }

            if (!appUser.EmailConfirmed)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var urlHelper = _urlHelper.GetUrlHelper();
                string link = $"{requestScheme}://{requestHost}/Verify/{HttpUtility.UrlEncode(appUser.Email)}/{HttpUtility.UrlEncode(token)}";
                string verificationMessageBody = string.Empty;
                using (StreamReader fileStream = new StreamReader("wwwroot/Verification/VerificationEmail.html"))
                {
                    verificationMessageBody = await fileStream.ReadToEndAsync();
                }
                verificationMessageBody = verificationMessageBody.Replace("{{link}}", link);
                verificationMessageBody = verificationMessageBody.Replace("{{userName}}", appUser.UserName);
                _sendEmail.Send("ilgarchsh@code.edu.az", "E-Commerce", appUser.Email, verificationMessageBody, "Confirm Account");
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = $"{appUser.UserName}  please verify email ,\n Token : {token}"
                };
            }

            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{appUser.UserName} profile succesfully updated"
            };
        }

        public async Task<ResponseObj> Register(AppUser appUser, string password, string requestScheme, string requestHost)
        {
            IdentityResult resoult = await _userManager.CreateAsync(appUser, password);
            if (!resoult.Succeeded)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description))
                };
            }
            await _userManager.AddToRoleAsync(appUser, appUser.IsSeller ? "Seller" : "User");
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var urlHelper = _urlHelper.GetUrlHelper();
            string link = $"{requestScheme}://{requestHost}/Verify/{HttpUtility.UrlEncode(appUser.Email)}/{HttpUtility.UrlEncode(token)}";
            string verificationMessageBody = string.Empty;
            using (StreamReader fileStream = new StreamReader("wwwroot/Verification/VerificationEmail.html"))
            {
                verificationMessageBody = await fileStream.ReadToEndAsync();
            }
            verificationMessageBody = verificationMessageBody.Replace("{{link}}", link);
            verificationMessageBody = verificationMessageBody.Replace("{{userName}}", appUser.UserName);
            Random random = new();
            int otp = random.Next(100000, 999999);
            verificationMessageBody = verificationMessageBody.Replace("{{OTP}}", otp.ToString());
            Dictionary<string, string> userCache = new();
            userCache.Add(appUser.Email, otp.ToString());
            await _distributedCache.SetStringAsync(appUser.UserName, JsonConvert.SerializeObject(userCache), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(2)
            });
            _sendEmail.Send("ilgarchsh@code.edu.az", "E-Commerce", appUser.Email, verificationMessageBody, "Confirm Account");
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{appUser.UserName}  please verify email ,\n Token : {token}"
            };

        }


        public async Task<ResponseObj> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(userResetPasswordDto.Email);
            if (appUser == null || appUser.IsDeleted) return new ResponseObj
            {
                ResponseMessage = "user not found",
                StatusCode = (int)StatusCodes.Status404NotFound
            };
            var isSucceeded = await _userManager.VerifyUserTokenAsync(appUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", userResetPasswordDto.Token);
            if (!isSucceeded) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "TokenIsNotValid"
            };
            IdentityResult resoult = await _userManager.ResetPasswordAsync(appUser, userResetPasswordDto.Token, userResetPasswordDto.Password);
            if (!resoult.Succeeded) return new ResponseObj
            {
                ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description)),
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            await _userManager.UpdateSecurityStampAsync(appUser);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "user password successfully reseted"
            };

        }
        public async Task<ResponseObj> VerifyEmailWithOTP(string VerifyEmail, string otp)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(VerifyEmail);
            if (appUser == null || appUser.IsDeleted)
            {
                return new ResponseObj
                {
                    ResponseMessage = "user is not exist",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            if (appUser.EmailConfirmed) return new ResponseObj
            {
                ResponseMessage = "user had alarady comfirmed",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            string userCache = await _distributedCache.GetStringAsync(appUser.UserName);
            if (userCache == null)
            {
                return new ResponseObj
                {
                    ResponseMessage = $"otp code is expired",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            Dictionary<string, string> userCacheResoult = JsonConvert.DeserializeObject<Dictionary<string, string>>(userCache);

            string otpResoult = userCacheResoult[appUser.Email];
            if (otpResoult != otp) return new ResponseObj
            {
                ResponseMessage = $"OTP code is wrong",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };

            appUser.EmailConfirmed = true;
            IdentityResult resoult = await _userManager.UpdateAsync(appUser);

            if (!resoult.Succeeded)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(e => e.Description))
                };
            }
            await _userManager.UpdateSecurityStampAsync(appUser);
            return new ResponseObj
            {
                ResponseMessage = $"{appUser.UserName} sucessfully verified",
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
        public async Task<ResponseObj> VerifyEmail(string VerifyEmail, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(VerifyEmail);
            if (appUser == null || appUser.IsDeleted)
            {
                return new ResponseObj
                {
                    ResponseMessage = "user is not exist",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            IdentityResult resoult = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!resoult.Succeeded)
            {
                return new ResponseObj
                {
                    ResponseMessage = string.Join(", ", resoult.Errors.Select(error => error.Description)),
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }
            await _userManager.UpdateSecurityStampAsync(appUser);
            return new ResponseObj
            {
                ResponseMessage = $"{appUser.UserName} sucessfully verified",
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
    }
}

