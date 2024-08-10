using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
	public class ChatMessageService : IChatMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
		public ChatMessageService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
		{
            _unitOfWork = unitOfWork;
            _userManager = userManager;
		}

        public async Task<ResponseObj> Create(ChatMessage entity)
        {
            try
            {
                if (entity.Message.Trim() == "") return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Message is not valid"
                };
                else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.FromUserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this user is not exist"
                };
                else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.ToUserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this user is not exist"
                };
                await _unitOfWork.ChatMessageRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = entity.Id
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
                    ResponseMessage = "Message is not exist"
                };
                ChatMessage chatMessage = await GetEntity(c => c.Id == id);
                chatMessage.IsDeleted = true;
                chatMessage.DeletedAt = DateTime.Now;
                await _unitOfWork.ChatMessageRepository.Update(chatMessage);
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = $"Message successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ChatMessage>> GetAll(Expression<Func<ChatMessage, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.ChatMessageRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ChatMessage> GetEntity(Expression<Func<ChatMessage, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.ChatMessageRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<ChatMessage, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.ChatMessageRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(ChatMessage entity)
        {
            try
            {
                if (!await IsExist(m=>m.Id==entity.Id)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Message is not exist"
                };

                else if (entity.Message.Trim() == "") return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Message is not valid"
                };
                else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.FromUserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this user is not exist"
                };
                else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.ToUserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "this user is not exist"
                };
                if (!entity.IsDeleted)
                {
                    entity.DeletedAt = null;
                }
                entity.UpdatedAt = DateTime.Now;
                await _unitOfWork.ChatMessageRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    ResponseMessage = entity.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }
    }
}

