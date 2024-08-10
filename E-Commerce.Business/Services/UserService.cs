using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.UserDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Services
{
	public class UserService:IUserService
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<ResponseObj> Delete(string id)
        {
            try
            {
                AppUser appUser = await _userManager.FindByIdAsync(id);
                if (appUser.IsDeleted) return new ResponseObj
                {
                    ResponseMessage="User is not exist",
                    StatusCode=(int)StatusCodes.Status404NotFound
                };
                else if (appUser == null) return new ResponseObj
                {
                    ResponseMessage = "User is not exist",
                    StatusCode = (int)StatusCodes.Status404NotFound
                };
                else if (await _userManager.IsInRoleAsync(appUser, "Admin") || await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
                {
                    ResponseMessage = "Something went wrong",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
                appUser.IsDeleted = true;
                appUser.RemovedAt = DateTime.Now;
                await _userManager.UpdateAsync(appUser);
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = $"{appUser.FullName} succesfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetUserDto>> GetAllUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<AppUser> query = _userManager.Users;
                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);
                }
                List<AppUser> users = predicate == null ?await query.ToListAsync() : await query.Where(predicate).ToListAsync();
                List <GetUserDto> getUserDtos =  _mapper.Map<List<GetUserDto>>(users);
                for (int i = 0; i < users.Count; i++)
                {
                    getUserDtos[i].Roles = await _userManager.GetRolesAsync(users[i]);
                }
                return getUserDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetUserDto> GetUser(Expression<Func<AppUser, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<AppUser> query = _userManager.Users;
                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);
                }
                if (predicate == null) return null;
                AppUser user = await query.FirstOrDefaultAsync(predicate);
               GetUserDto getUserDto = _mapper.Map<GetUserDto>(user);
                getUserDto.Roles = await _userManager.GetRolesAsync(user);
                return getUserDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(string id, UpdateUserDto updateUserDto)
        {
            try
            {
                AppUser appUser = await _userManager.FindByIdAsync(id);
                var roles =await _roleManager.Roles.ToListAsync();
                foreach (var role in updateUserDto.Roles)
                {
                    if (!roles.Any(r => r.ToString() == role)) return new ResponseObj
                    {
                        ResponseMessage = "this role is not exist",
                        StatusCode=(int)StatusCodes.Status400BadRequest
                    };
                }
                 if (await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Supper admin can not update"
                };
                else if (updateUserDto.Roles.Any(r => r == "SupperAdmin")) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "SupperAdmin role can be belong to only 1 user"
                };
                UpdateUserDto existUserUpdateDto = _mapper.Map<UpdateUserDto>(appUser);
                existUserUpdateDto.Roles = await _userManager.GetRolesAsync(appUser);
                if (updateUserDto.Roles == null || updateUserDto.Roles.Count == 0) return new ResponseObj
                {
                    ResponseMessage = "Roles Must not be Null",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
                _mapper.Map(updateUserDto, appUser);
                if (updateUserDto.IsDeleted)
                {

                     if (await _userManager.IsInRoleAsync(appUser, "Admin") || await _userManager.IsInRoleAsync(appUser, "SupperAdmin")) return new ResponseObj
                    {
                        ResponseMessage = "Something went wrong",
                        StatusCode = (int)StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    appUser.RemovedAt = null;
                }

                await _userManager.RemoveFromRolesAsync(appUser, existUserUpdateDto.Roles);

                appUser.UpdatedAt = DateTime.Now;
                await _userManager.AddToRolesAsync(appUser, updateUserDto.Roles);
                if (await _userManager.IsInRoleAsync(appUser, "Seller")) 
                {
                    appUser.IsSeller = true;
                }
                else
                {
                    appUser.IsSeller = false;
                }
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded) return new ResponseObj
                {
                    ResponseMessage = string.Join(", ", result.Errors.Select(e => e.Description)),
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
                return new ResponseObj
                {
                    ResponseMessage = $"{appUser.FullName} successfully updated",
                    StatusCode = (int)StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> IsExist(Expression<Func<AppUser, bool>> predicate = null)
        {
            try
            {
                return predicate == null ? false : await _userManager.Users.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IQueryable<AppUser>  GetAllIncludes(params string[] includes)
        {
            try
            {
                IQueryable<AppUser> query = _userManager.Users;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

