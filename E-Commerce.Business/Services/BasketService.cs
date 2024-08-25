using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public BasketService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<ResponseObj> DecreaseCount(Basket entity)
        {
            if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!await _unitOfWork.ProductRepository.IsExist(p => p.Id == entity.ProductId && !p.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this Product is not exist"
            };
            else if (!await IsExist(b => b.ProductId == entity.ProductId && !b.IsDeleted && b.UserId == entity.UserId))
            {

                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = " Basket is not exist "
                };
            }
            if (entity.Count <= 1)
            {
                ResponseObj responseObj = await Delete(entity.Id);
                await SaveChanges();
                return responseObj;
            }
            entity.Count -= 1;
            await _unitOfWork.BasketRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = " Basket count is successfully Decreased"
            };
        }
        public async Task<ResponseObj> IncreaseCount(Basket entity)
        {
            if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!await _unitOfWork.ProductRepository.IsExist(p => p.Id == entity.ProductId && !p.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this Product is not exist"
            };
            else if (!await IsExist(b => b.ProductId == entity.ProductId && !b.IsDeleted && b.UserId == entity.UserId))
            {

                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = " Basket is not exist "
                };
            }
            entity.Count += 1;
            await _unitOfWork.BasketRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = " Basket count is successfully Increased"
            };
        }
        public async Task<ResponseObj> Create(Basket entity)
        {
            if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!await _unitOfWork.ProductRepository.IsExist(p => p.Id == entity.ProductId && !p.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this Product is not exist"
            };
            else if (await IsExist(b => b.ProductId == entity.ProductId && !b.IsDeleted && b.UserId == entity.UserId))
            {
                entity = await GetEntity(b => b.ProductId == entity.ProductId && !b.IsDeleted && b.UserId == entity.UserId);
                entity.Count += 1;
                await _unitOfWork.BasketRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "this product is successfully Added to basket"
                };
            }
            entity.Count = 1;
            await _unitOfWork.BasketRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "this Product is successfully Added to basket"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(b => b.Id == id && !b.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "basket is not exist"
            };
            Basket basket = await GetEntity(b => b.Id == id);
            await _unitOfWork.BasketRepository.Delete(basket);

            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"basket successfully deleted"
            };
        }
        public async Task<int> SaveChanges()
        {
            return await _unitOfWork.Complate();

        }
        public async Task<List<Basket>> GetAll(Expression<Func<Basket, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BasketRepository.GetAll(predicate, includes);
        }

        public async Task<Basket> GetEntity(Expression<Func<Basket, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BasketRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Basket, bool>> predicate = null)
        {
            return await _unitOfWork.BasketRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Basket entity)
        {
            if (!await IsExist(b => b.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this basket is not  exist"
            };
            entity.UpdatedAt = DateTime.Now;
            await _unitOfWork.BasketRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "this basket   successfully updated"
            };
        }
    }
}

