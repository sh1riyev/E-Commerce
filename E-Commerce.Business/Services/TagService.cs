using System;
using System.Linq.Expressions;
using E_Commerce.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using   E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Core.Entities;
using   E_Commerce.Data;

namespace E_Commerce.Business.Services
{
	public class TagService:ITagService
	{
        private readonly IUnitOfWork _unitOfWork;
        public TagService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<ResponseObj> Create(Tag entity)
        {
            try
            {
                if (await IsExist(t => t.Name.ToLower() == entity.Name.ToLower())) return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status400BadRequest,
                    ResponseMessage="This tag name is exist"
                };
                await _unitOfWork.TagRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Tag successfully created"
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
                if (!await IsExist(t => t.Id == id&&!t.IsDeleted)) return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status400BadRequest,
                    ResponseMessage="Tag is not exist"
                };
                Tag tag = await GetEntity(t => t.Id == id);
                tag.IsDeleted = true;
                tag.DeletedAt = DateTime.Now;
                ResponseObj responseObj = await Update(tag);
                if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
                {
                    return responseObj;
                }
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Category succesfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Tag>> GetAll(Expression<Func<Tag, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.TagRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tag> GetEntity(Expression<Func<Tag, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.TagRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Tag, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.TagRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Tag entity)
        {
            try
            {
                if (await IsExist(t => t.Name.ToLower() == entity.Name.ToLower()&&t.Id!=entity.Id)) return new ResponseObj
                {
                    StatusCode=StatusCodes.Status400BadRequest,
                    ResponseMessage="this tag is exist"
                };
                if (!entity.IsDeleted)
                {
                    entity.DeletedAt = null;
                }
                entity.UpdatedAt = DateTime.Now;
                await _unitOfWork.TagRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Tag successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

