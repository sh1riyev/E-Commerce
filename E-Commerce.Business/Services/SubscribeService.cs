using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data;
using E_Commerce.Data.Implimentations;

namespace E_Commerce.Business.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubscribeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Subscribe entity)
        {
            if (await IsExist(s => s.Email.ToLower() == entity.Email.ToLower())) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "this email was subscribed"
            };
            await _unitOfWork.SubscribeRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "this email is successfully subscribed"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(s => s.Id == id && !s.IsDeleted)) return new ResponseObj
            {
                ResponseMessage = "subscribe is not exist",
                StatusCode = (int)StatusCodes.Status400BadRequest
            };
            Subscribe subscribe = await GetEntity(s => s.Id == id);
            subscribe.DeletedAt = DateTime.Now;
            subscribe.IsDeleted = true;
            ResponseObj responseObj = await Update(subscribe);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                return responseObj;
            }
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "subscribe is successfully deleted"
            };
        }

        public async Task<List<Subscribe>> GetAll(Expression<Func<Subscribe, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.SubscribeRepository.GetAll(predicate, includes);
        }

        public async Task<Subscribe> GetEntity(Expression<Func<Subscribe, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.SubscribeRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Subscribe, bool>> predicate = null)
        {
            return await _unitOfWork.SubscribeRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Subscribe entity)
        {
            if (await IsExist(s => s.Email.ToLower() == entity.Email.ToLower() && s.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "this email was subscribed"
            };
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            entity.UpdatedAt = DateTime.Now;
            await _unitOfWork.SubscribeRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "this subscribed is successfully updated"
            };
        }
    }
}

