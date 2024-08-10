using System;
using System.Linq.Expressions;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.UserDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Interfaces
{
	public interface IUserService
	{
		Task<List<GetUserDto>> GetAllUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes);
		Task<ResponseObj> Delete(string id);
		Task<GetUserDto> GetUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes);
		Task<ResponseObj> Update(string id, UpdateUserDto updateUserDto);
        Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null);
    }
}

