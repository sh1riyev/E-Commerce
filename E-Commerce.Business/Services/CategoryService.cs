using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Repositories;
using E_Commerce.Data;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Services
{
	public class CategoryService:ICategoryService
	{
        private readonly IUnitOfWork _unitOfWork;
		public CategoryService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<ResponseObj> Create(Category entity)
        {
            try
            {
                if (await IsExist(c=>c.Name.ToLower()==entity.Name.ToLower()))
                {
                    return new ResponseObj {
                        StatusCode= (int)StatusCodes.Status400BadRequest ,
                        ResponseMessage="Category is exist"
                    };
                }
                else if (entity.ParentId!=null&&!await IsExist(c=>c.Id==entity.ParentId&&!c.IsDeleted&&c.IsMain))
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
                    StatusCode= (int)StatusCodes.Status200OK,
                    ResponseMessage= "Category succesfully created"
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
                if (! await IsExist(c=>c.Id==id))
                {
                    return new ResponseObj{
                        StatusCode = (int)StatusCodes.Status404NotFound,
                        ResponseMessage= "Category is not exist"
                    };
                }
                Category categoty = await GetEntity(c => c.Id == id);
                 if (categoty.IsDeleted)
                {
                    return new ResponseObj
                    {
                        StatusCode= (int)StatusCodes.Status400BadRequest,
                        ResponseMessage="Category is not active"
                    };
                }
                categoty.IsDeleted = true;
                categoty.DeletedAt = DateTime.Now;
                var response=await Update(categoty);
                if (response.StatusCode!=(int)StatusCodes.Status200OK)
                {
                    return response;
                }
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Category succesfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CategoryRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetEntity(Expression<Func<Category, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CategoryRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Category, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.CategoryRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Category entity)
        {

            try
            {
                entity.UpdatedAt = DateTime.Now;
                if (await IsExist(c=>c.Name.ToLower()==entity.Name.ToLower()&&entity.Id!=c.Id))
                {
                    return new ResponseObj
                    {
                        StatusCode= (int)StatusCodes.Status400BadRequest,
                        ResponseMessage="Category is exist"
                    };
                }
                else if (entity.ParentId!=null&& !await IsExist(c => c.Id == entity.ParentId && !c.IsDeleted && c.IsMain))
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
                    StatusCode= (int)StatusCodes.Status200OK,
                    ResponseMessage="Category successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

