using System;
using E_Commerce.Core.Entities;
using System.Linq.Expressions;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.AccountDto;

namespace E_Commerce.Business.Interfaces
{
	public interface IAccountService
	{
        Task<ResponseObj> Register(AppUser appUser, string password, string requestScheme, string requestHost);
        Task<ResponseObj> Register(AppUser appUser, string password);
        Task<ResponseObj> Login(UserLoginDto loginDto);
        Task<ResponseObj> ForgetPassword(string email, string requestScheme, string requestHost);
        Task<ResponseObj> ResetPassword(UserResetPasswordDto userResetPasswordDto);
        Task<ResponseObj> VerifyEmail(string VerifyEmail, string token);
        Task<ResponseObj> VerifyEmailWithOTP(string VerifyEmail, string otp);
        Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null);
        Task<ResponseObj> ProfileUpdate(string id, UserUpdateDto userUpdateDto, string requestScheme, string requestHost);
    }
}

