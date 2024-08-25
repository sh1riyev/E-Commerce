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

    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public BlogService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseObj> Create(Blog entity)
        {
            if (await IsExist(b => b.Title.ToLower() == entity.Title.ToLower())) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Title} is exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            foreach (var tag in entity.BlogTags)
            {
                if (!await _unitOfWork.TagRepository.IsExist(t => t.Id == tag.TagId && !t.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "tag is not exist"
                };
            }
            foreach (var tag in entity.BlogTags)
            {
                await _unitOfWork.BlogTagRepository.Create(tag);
            }
            await _unitOfWork.BlogRepository.Create(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{entity.Title} successfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(b => b.Id == id && !b.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "blog is not exist"
            };
            Blog blog = await GetEntity(b => b.Id == id);
            blog.IsDeleted = true;
            blog.DeletedAt = DateTime.Now;
            await _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{blog.Title} successfully deleted"
            };
        }

        public async Task<List<Blog>> GetAll(Expression<Func<Blog, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BlogRepository.GetAll(predicate, includes);
        }

        public async Task<Blog> GetEntity(Expression<Func<Blog, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.BlogRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Blog, bool>> predicate = null)
        {
            return await _unitOfWork.BlogRepository.IsExist(predicate);
        }

        public async Task<ResponseObj> Update(Blog entity)
        {
            if (await IsExist(b => b.Title.ToLower() == entity.Title.ToLower() && b.Id != entity.Id)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = $"{entity.Title} is exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.UserId && !u.IsDeleted && u.IsActive)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            if (entity.BlogTags.Count > 0)
            {
                foreach (var tag in entity.BlogTags)
                {
                    if (!await _unitOfWork.TagRepository.IsExist(t => t.Id == tag.TagId && !t.IsDeleted)) return new ResponseObj
                    {
                        StatusCode = (int)StatusCodes.Status404NotFound,
                        ResponseMessage = "tag is not exist"
                    };
                }
            }


            if (entity.BlogTags.Count > 0)
            {
                List<BlogTags> blogTags = await _unitOfWork.BlogTagRepository.GetAll(pt => pt.BlogId == entity.Id);
                foreach (var tag in blogTags)
                {
                    await _unitOfWork.BlogTagRepository.Delete(tag);
                }
                foreach (var tag in entity.BlogTags.FindAll(pt => pt.Blog == null))
                {
                    await _unitOfWork.BlogTagRepository.Create(tag);
                }
            }
            entity.UpdatedAt = DateTime.Now;
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.BlogRepository.Update(entity);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{entity.Title} successfully updated"
            };
        }
    }
}

