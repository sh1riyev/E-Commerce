using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
	public class ContactService:IContactService
	{
        private readonly IUnitOfWork _unitOfWork;
		public ContactService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public async Task<ResponseObj> Create(Contact entity)
        {
            try
            {
                entity.IsResponded = false;
                await _unitOfWork.ContactRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Contact successfully created"
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
                if (!await IsExist(c => c.Id == id && !c.IsDeleted)) return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status404NotFound,
                    ResponseMessage="contact not found"
                };
                Contact contact = await GetEntity(c => c.Id == id);
                contact.DeletedAt = DateTime.Now;
                contact.IsDeleted = true;
                ResponseObj response = await Update(contact);
                if (response.StatusCode != (int)StatusCodes.Status200OK)
                {
                    return response;
                }
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = "Contact succesfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Contact>> GetAll(Expression<Func<Contact, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.ContactRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Contact> GetEntity(Expression<Func<Contact, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.ContactRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Contact, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.ContactRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(Contact entity)
        {
            try
            {
                entity.UpdatedAt = DateTime.Now;
                if (!entity.IsDeleted)
                {
                    entity.DeletedAt = null;
                }
                if (!entity.IsResponded)
                {
                    entity.RespondedAt = null;
                }
                await _unitOfWork.ContactRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Contact successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

