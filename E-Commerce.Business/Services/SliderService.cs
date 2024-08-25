using System;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Core.Entities;
using E_Commerce.Data;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using E_Commerce.Business.Interfaces;

namespace E_Commerce.Business.Services
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SliderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseObj> Create(Slider entity)
        {
            await _unitOfWork.SliderRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Slider Successfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(s => s.Id == id && !s.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Slider does not exist"
            };
            Slider slider = await GetEntity(s => s.Id == id);
            slider.IsDeleted = true;
            slider.DeletedAt = DateTime.Now;
            ResponseObj responseObj = await Update(slider);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK) return responseObj;
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Slider successfully deleted"
            };
        }

        public async Task<List<Slider>> GetAll(Expression<Func<Slider, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.SliderRepository.GetAll(predicate, includes);
        }

        public async Task<Slider> GetEntity(Expression<Func<Slider, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.SliderRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Slider, bool>> predicate = null)
        {
            return await _unitOfWork.SliderRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Slider entity)
        {
            if (!await IsExist(s => s.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Slider is not exist"
            };
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            entity.UpdatedAt = DateTime.Now;
            await _unitOfWork.SliderRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Slider successfully updated"
            };
        }
    }
}

