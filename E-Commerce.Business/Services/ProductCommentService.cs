using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;
using Product = E_Commerce.Core.Entities.Product;

namespace E_Commerce.Business.Services
{
	public class ProductCommentService:IProductCommentService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
		public ProductCommentService(IUnitOfWork unitOfWork,UserManager<AppUser>userManager)
		{
            _unitOfWork = unitOfWork;
            _userManager = userManager;
		}

        public async Task<ResponseObj> Create(ProductComment entity)
        {
            try
            {
                if (!await _unitOfWork.ProductRepository.IsExist(p => p.Id == entity.ProductId&&!p.IsDeleted)) return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status404NotFound,
                    ResponseMessage="Product is not exist"
                };
                else if (!await _userManager.Users.AnyAsync(u=>u.Id==entity.UserId&&!u.IsDeleted&&u.IsActive)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "User is not exist"
                };
                
                Product product = await _unitOfWork.ProductRepository.GetEntity(p => p.Id == entity.ProductId);
                List<ProductComment> comments = await _unitOfWork.ProductCommentRepository.GetAll(c => !c.IsDeleted && c.ProductId == product.Id);
                comments.Add(entity);
                product.StarsCount = (int)(Math.Floor(comments.Sum(c => c.Rating) / comments.Count));
                await _unitOfWork.ProductRepository.Update(product);
                await _unitOfWork.ProductCommentRepository.Create(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Comment Successfully created"
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
                if (!await IsExist(pc => pc.Id == id&&!pc.IsDeleted)) return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status404NotFound,
                    ResponseMessage="Comment is not exist"
                };
                ProductComment productComment = await GetEntity(pc => pc.Id == id);
                productComment.IsDeleted = true;
                productComment.DeletedAt = DateTime.Now;
                await _unitOfWork.ProductCommentRepository.Update(productComment);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Comment successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProductComment>> GetAll(Expression<Func<ProductComment, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.ProductCommentRepository.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductComment> GetEntity(Expression<Func<ProductComment, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _unitOfWork.ProductCommentRepository.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<ProductComment, bool>> predicate = null)
        {
            try
            {
                return await _unitOfWork.ProductCommentRepository.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseObj> Update(ProductComment entity)
        {
            try
            {
                if (!await IsExist(pc => pc.Id == entity.Id)) return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status404NotFound,
                    ResponseMessage="Comment is not exist"
                };
                else if (!await _unitOfWork.ProductRepository.IsExist(p => p.Id == entity.ProductId && !p.IsDeleted)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "Product is not exist"
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
                await _unitOfWork.ProductCommentRepository.Update(entity);
                await _unitOfWork.Complate();
                return new ResponseObj
                {
                    StatusCode=(int)StatusCodes.Status200OK,
                    ResponseMessage="Comment successfully updated"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

