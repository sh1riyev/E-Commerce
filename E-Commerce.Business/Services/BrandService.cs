using System;
using System.Linq.Expressions;
using E_Commerce.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Brand entity)
        {
            if (await IsExist(b => b.Name.ToLower() == entity.Name.ToLower()))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Brand is exist"
                };
            }
            await _unitOfWork.BrandRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Brand succesfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(b => b.Id == id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Brand is not exist"
                };
            }
            Brand brand = await GetEntity(b => b.Id == id);
            if (brand.IsDeleted)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Brand is not active"
                };
            }
            brand.IsDeleted = true;
            brand.DeletedAt = DateTime.Now;
            var response = await Update(brand);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                return response;
            }
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Brand succesfully deleted"
            };
        }

        public async Task<List<Brand>> GetAll(Expression<Func<Brand, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BrandRepository.GetAll(predicate, includes);
        }

        public async Task<Brand> GetEntity(Expression<Func<Brand, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BrandRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Brand, bool>> predicate = null)
        {
            return await _unitOfWork.BrandRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Brand entity)
        {
            entity.UpdatedAt = DateTime.Now;
            if (await IsExist(b => b.Name.ToLower() == entity.Name.ToLower() && entity.Id != b.Id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Brand is exist"
                };
            }
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.BrandRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Brand successfully updated"
            };
        }
    }
}

