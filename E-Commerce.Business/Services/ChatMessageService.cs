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
        public ChatMessageService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseObj> Create(ChatMessage entity)
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

        public async Task<ResponseObj> Delete(string id)
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

        public async Task<List<ChatMessage>> GetAll(Expression<Func<ChatMessage, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.ChatMessageRepository.GetAll(predicate, includes);
        }

        public async Task<ChatMessage> GetEntity(Expression<Func<ChatMessage, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.ChatMessageRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<ChatMessage, bool>> predicate = null)
        {
            return await _unitOfWork.ChatMessageRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(ChatMessage entity)
        {
            if (!await IsExist(m => m.Id == entity.Id)) return new ResponseObj
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
    }
}

