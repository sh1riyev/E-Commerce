using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
	public class CompaignsService: ICompaignsService
    {
        private readonly IUnitOfWork _unitOfWork;
		public CompaignsService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<ResponseObj> Create(Campaign entity)
        {
            try
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
                await _unitOfWork.CampaignsRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Compaign succesfully created"
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
                if (!await IsExist(c => c.Id == id))
                {
                    return new ResponseObj
                    {
                        StatusCode = (int)StatusCodes.Status404NotFound,
                        ResponseMessage = "Compaign is not exist"
                    };
                }
                Campaign compaign = await GetEntity(c => c.Id == id);
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
                await _unitOfWork.CampaignsRepository.Update(compaign);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Compaign succesfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<List<Campaign>> GetAll(Expression<Func<Campaign, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CampaignsRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Campaign> GetEntity(Expression<Func<Campaign, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CampaignsRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Campaign, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.CampaignsRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Campaign entity)
        {
            try
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
                await _unitOfWork.CampaignsRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Compaign successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

