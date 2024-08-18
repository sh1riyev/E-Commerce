using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
	public class CheckService:ICheckService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
		public CheckService(IUnitOfWork unitOfWork,UserManager<AppUser>userManager)
		{
            _unitOfWork = unitOfWork;
            _userManager = userManager;
		}

        public async Task<ResponseObj> Create(Check entity)
        {
            try
            {
                if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this user is not exist"
                };
                else if (!await _unitOfWork.AdressRepository.IsExist(a => a.Id == entity.AdressId && !a.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this adress is not exist"
                };
                await _unitOfWork.CheckRepository.Create(entity);
                foreach (var checkProduct in entity.CheckProducts)
                {
                    await _unitOfWork.CheckProductRepository.Create(checkProduct);
                }
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Check successfully added"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Delete(string id)
        {
            try
            {
                if (!await IsExist(c => c.Id == id && !c.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Check is not exist"
                };
                Check check = await GetEntity(c => c.Id == id);
                check.IsDeleted = true;
                check.DeletedAt = DateTime.Now;
                await _unitOfWork.CheckRepository.Update(check);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Check  successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<List<Check>> GetAll(Expression<Func<Check, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CheckRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Check> GetEntity(Expression<Func<Check, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CheckRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Check, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.CheckRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Check entity)
        {
            if (!await IsExist(c => c.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Check is not exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!await _unitOfWork.AdressRepository.IsExist(a => a.Id == entity.AdressId && !a.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this adress is not exist"
            };
            if (entity.Status==(int)UserOrderStatus.Delivered)
            {
                entity.DeliverAt = DateTime.Now;
            }
            else
            {
                entity.DeliverAt =null;
            }
            entity.UpdatedAt = DateTime.Now;
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.CheckRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Check  successfully updated"
            };
        }
    }
}



