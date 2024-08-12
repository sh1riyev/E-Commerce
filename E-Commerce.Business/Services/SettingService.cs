using System;
using E_Commerce.Business.DTOs.ResponseDto;
using System.Linq.Expressions;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.Services
{
	public class SettingService : ISettingService
	{
        private readonly IUnitOfWork _unitOfWork;
        public SettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Setting entity)
        {
            try
            {
                if (await IsExist(s => s.Key.ToLower() == entity.Key.ToLower())) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "this key is exist"
                };
                await _unitOfWork.SettingRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = $"{entity.Key} successfully created"
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
                if (!await IsExist(s => s.Id == id && !s.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "setting is not exist"
                };
                Setting setting = await GetEntity(s => s.Id == id);
                setting.DeletedAt = DateTime.Now;
                setting.IsDeleted = true;
                ResponseObj responseObj = await Update(setting);
                if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
                {
                    return responseObj;
                }
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = $"{setting.Key} successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Setting>> GetAll(Expression<Func<Setting, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.SettingRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Setting> GetEntity(Expression<Func<Setting, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.SettingRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Setting, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.SettingRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Setting entity)
        {
            try
            {
                if (!await IsExist(s => s.Id == entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this setting is not exist"
                };
                else if (await IsExist(s => s.Key.ToLower() == entity.Key.ToLower() && s.Id != entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    ResponseMessage = "this Key is exist"
                };
                entity.UpdatedAt = DateTime.Now;
                if (!entity.IsDeleted)
                {
                    entity.DeletedAt = null;
                }
                await _unitOfWork.SettingRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = $"{entity.Key} successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

