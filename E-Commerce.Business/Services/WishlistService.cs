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
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public WishlistService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseObj> Create(Wishlist entity)
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
            else if (await IsExist(w => w.ProductId == entity.ProductId && !w.IsDeleted && w.UserId == entity.UserId)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this wishlist is  exist"
            };
            await _unitOfWork.WishlistRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "this wishlist is successfully created"
            };
        }
        public async Task<int> SaveChanges()
        {
            return await _unitOfWork.Complate();
        }
        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(w => w.Id == id && !w.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "wishlist is not exist"
            };
            Wishlist wishlist = await GetEntity(w => w.Id == id);
            await _unitOfWork.WishlistRepository.Delete(wishlist);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"wishlist successfully deleted"
            };
        }

        public async Task<List<Wishlist>> GetAll(Expression<Func<Wishlist, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.WishlistRepository.GetAll(predicate, includes);
        }

        public async Task<Wishlist> GetEntity(Expression<Func<Wishlist, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.WishlistRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Wishlist, bool>> predicate = null)
        {
            return await _unitOfWork.WishlistRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Wishlist entity)
        {
            if (!await IsExist(w => w.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this wishlist is not  exist"
            };
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            entity.UpdatedAt = DateTime.Now;
            await _unitOfWork.WishlistRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "this wishlist   successfully updated"
            };
        }
    }
}

