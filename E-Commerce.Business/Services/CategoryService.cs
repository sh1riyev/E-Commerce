using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Data;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Category entity)
        {
            if (await IsExist(c => c.Name.ToLower() == entity.Name.ToLower()))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Category is exist"
                };
            }
            else if (entity.ParentId != null && !await IsExist(c => c.Id == entity.ParentId && !c.IsDeleted && c.IsMain))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Not found category in this parent id"
                };
            }
            await _unitOfWork.CategoryRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Category succesfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(c => c.Id == id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Category is not exist"
                };
            }
            Category categoty = await GetEntity(c => c.Id == id);
            if (categoty.IsDeleted)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Category is not active"
                };
            }
            categoty.IsDeleted = true;
            categoty.DeletedAt = DateTime.Now;
            var response = await Update(categoty);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                return response;
            }
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Category succesfully deleted"
            };
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CategoryRepository.GetAll(predicate, includes);
        }

        public async Task<Category> GetEntity(Expression<Func<Category, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CategoryRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Category, bool>> predicate = null)
        {
            return await _unitOfWork.CategoryRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Category entity)
        {

            entity.UpdatedAt = DateTime.Now;
            if (await IsExist(c => c.Name.ToLower() == entity.Name.ToLower() && entity.Id != c.Id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Category is exist"
                };
            }
            else if (entity.ParentId != null && !await IsExist(c => c.Id == entity.ParentId && !c.IsDeleted && c.IsMain))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Not found category in this parent id"
                };
            }
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.CategoryRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Category successfully updated"
            };
        }
    }
}

