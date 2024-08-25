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
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public BlogCommentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseObj> Create(BlogComment entity)
        {
            if (!await _unitOfWork.BlogRepository.IsExist(b => b.Id == entity.BlogId && !b.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Blog is not exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "User is not exist"
            };
            else if (entity.ParentId != null)
            {
                if (!await IsExist(c => c.Id == entity.ParentId && !c.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Parent is not exist"
                };
            }
            await _unitOfWork.BlogCommentRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Comment Successfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(c => c.Id == id && !c.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Comment is not exist"
            };
            BlogComment blogComment = await GetEntity(c => c.Id == id);
            blogComment.IsDeleted = true;
            blogComment.DeletedAt = DateTime.Now;
            await _unitOfWork.BlogCommentRepository.Update(blogComment);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Comment successfully deleted"
            };
        }

        public async Task<List<BlogComment>> GetAll(Expression<Func<BlogComment, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BlogCommentRepository.GetAll(predicate, includes);
        }

        public async Task<BlogComment> GetEntity(Expression<Func<BlogComment, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BlogCommentRepository.GetEntity(predicate, includes);

        }

        public async Task<bool> IsExist(Expression<Func<BlogComment, bool>> predicate = null)
        {
            return await _unitOfWork.BlogCommentRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(BlogComment entity)
        {
            if (!await IsExist(c => c.Id == entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Comment is not exist"
            };
            else if (!await _unitOfWork.BlogRepository.IsExist(b => b.Id == entity.BlogId && !b.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Blog is not exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "User is not exist"
            };
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            entity.UpdatedAt = DateTime.Now;
            await _unitOfWork.BlogCommentRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = "Comment successfully updated"
            };
        }
    }
}

