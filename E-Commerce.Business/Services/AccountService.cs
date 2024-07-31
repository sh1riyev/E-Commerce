using System;
using System.Linq.Expressions;
using AutoMapper;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Web_Api.Business.DTO.AccountDto;

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
        private readonly string _publicUrl = "https://rewear.site";
        private readonly string _privateUrl = "http://localhost:8080";
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

        public Task<ResponseObj> ForgetPassword(string email, string requestScheme, string requestHost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> Login(UserLoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> ProfileUpdate(string id, UserUpdateDto userUpdateDto, string requestScheme, string requestHost)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> Register(AppUser appUser, string password, string requestScheme, string requestHost)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> Register(AppUser appUser, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> VerifyEmail(string VerifyEmail, string token)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObj> VerifyEmailWithOTP(string VerifyEmail, string otp)
        {
            throw new NotImplementedException();
        }
    }
}

