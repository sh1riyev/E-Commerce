using System;
using E_Commerce.Business.DTOs.ResponseDto;
using System.Linq.Expressions;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.Services
{
	public class CityService : ICityService
	{
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(City entity)
        {
            try
            {
                if (await IsExist(c => c.Name.ToLower() == entity.Name.ToLower() && c.CountryId == entity.CountryId)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this City already had created"
                };
                else if (!await _unitOfWork.CountryRepository.IsExist(c => c.Id == entity.CountryId && !c.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this Country is not exist"
                };
                await _unitOfWork.CityRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "City successfully added"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<ResponseObj> Delete(string id)
        {
            try
            {
                if (!await IsExist(c => c.Id == id && !c.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "City is not exist"
                };
                City city = await GetEntity(c => c.Id == id);
                city.IsDeleted = true;
                city.DeletedAt = DateTime.Now;
                await _unitOfWork.CityRepository.Update(city);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "City  successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<List<City>> GetAll(Expression<Func<City, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CityRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City> GetEntity(Expression<Func<City, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CityRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<City, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.CityRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(City entity)
        {
            try
            {
                if (!await IsExist(c => c.Id == entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "City is not exist"
                };
                else if (await IsExist(c => c.Name.ToLower() == entity.Name.ToLower() && c.CountryId == entity.CountryId && c.Id != entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this City already had created"
                };
                else if (!await _unitOfWork.CountryRepository.IsExist(c => c.Id == entity.CountryId && !c.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this Country is not exist"
                };
                entity.UpdatedAt = DateTime.Now;
                if (!entity.IsDeleted)
                {
                    entity.DeletedAt = null;
                }
                await _unitOfWork.CityRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "City  successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }
    }
}

