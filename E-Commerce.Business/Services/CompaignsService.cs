using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
    public class CompaignsService : ICompaignsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompaignsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Compaigns entity)
        {
            if (await IsExist(c => c.Headling.ToLower() == entity.Headling.ToLower()))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Compaign is exist"
                };
            }
            else if (await IsExist(c => c.Info.ToLower() == entity.Info.ToLower()))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Compaign is exist"
                };
            }
            await _unitOfWork.CompaignsRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Compaign succesfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(c => c.Id == id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Compaign is not exist"
                };
            }
            Compaigns compaign = await GetEntity(c => c.Id == id);
            if (compaign.IsDeleted)
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Compaign is not active"
                };
            }
            compaign.IsDeleted = true;
            compaign.DeletedAt = DateTime.Now;
            await _unitOfWork.CompaignsRepository.Update(compaign);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Compaign succesfully deleted"
            };
        }

        public async Task<List<Compaigns>> GetAll(Expression<Func<Compaigns, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CompaignsRepository.GetAll(predicate, includes);
        }

        public async Task<Compaigns> GetEntity(Expression<Func<Compaigns, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.CompaignsRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Compaigns, bool>> predicate = null)
        {
            return await _unitOfWork.CompaignsRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Compaigns entity)
        {
            entity.UpdatedAt = DateTime.Now;
            if (await IsExist(c => c.Headling.ToLower() == entity.Headling.ToLower() && entity.Id != c.Id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Compaign is exist"
                };
            }
            else if (await IsExist(c => c.Info.ToLower() == entity.Info.ToLower() && entity.Id != c.Id))
            {
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "Compaign is exist"
                };
            }
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.CompaignsRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Compaign successfully updated"
            };
        }
    }
}

