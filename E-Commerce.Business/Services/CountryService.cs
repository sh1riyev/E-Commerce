using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
	public class CountryService:ICountryService
	{
        private readonly IUnitOfWork _unitOfWork;
		public CountryService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<ResponseObj> Create(Country entity)
        {
            try
            {
                if (await IsExist(c => c.Name.ToLower() == entity.Name.ToLower())) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this Country already had created"
                };
                await _unitOfWork.CountryRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Country successfully added"
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
                    ResponseMessage = "Country is not exist"
                };
                Country country = await GetEntity(c => c.Id == id);
                country.IsDeleted = true;
                country.DeletedAt = DateTime.Now;
                await _unitOfWork.CountryRepository.Update(country);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Country  successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<List<Country>> GetAll(Expression<Func<Country, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CountryRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Country> GetEntity(Expression<Func<Country, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.CountryRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Country, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.CountryRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Country entity)
        {
            try
            {
                if (!await IsExist(c => c.Id == entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Country is not exist"
                };
                else if (await IsExist(c => c.Name.ToLower() == entity.Name.ToLower() && c.Id != entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this Country already had created"
                };
                entity.UpdatedAt = DateTime.Now;
                if (!entity.IsDeleted)
                {
                    entity.DeletedAt = null;
                }
                await _unitOfWork.CountryRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Country  successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }
    }
}

