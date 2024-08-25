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
    public class AdressService : IAdressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public AdressService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseObj> Create(Adress entity)
        {
            if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!await _unitOfWork.CityRepository.IsExist(c => c.Id == entity.CityId && !c.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this City is not exist"
            };
            await _unitOfWork.AdressRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Address successfully added"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(a => a.Id == id && !a.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Address is not exist"
            };
            Adress adress = await GetEntity(a => a.Id == id);
            adress.IsDeleted = true;
            adress.DeletedAt = DateTime.Now;
            await _unitOfWork.AdressRepository.Update(adress);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Address  successfully deleted"
            };
        }

        public async Task<List<Adress>> GetAll(Expression<Func<Adress, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.AdressRepository.GetAll(predicate, includes);
        }

        public async Task<Adress> GetEntity(Expression<Func<Adress, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.AdressRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Adress, bool>> predicate = null)
        {
            return await _unitOfWork.AdressRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Adress entity)
        {
            if (!await IsExist(a => a.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Address is not exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!await _unitOfWork.CityRepository.IsExist(c => c.Id == entity.CityId && !c.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this City is not exist"
            };
            entity.UpdatedAt = DateTime.Now;
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.AdressRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Address  successfully updated"
            };
        }
    }
}

