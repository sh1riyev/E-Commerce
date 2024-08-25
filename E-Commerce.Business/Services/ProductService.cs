using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Data;

namespace E_Commerce.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISendEmail _sendEmail;
        private readonly string _publicUrl = "https://localhost:7052";
        private readonly string _privateUrl = "https://localhost:7052";
        public ProductService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ISendEmail sendEmail)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _sendEmail = sendEmail;
        }

        public async Task<ResponseObj> Create(Product entity)
        {
            if (!await _unitOfWork.BrandRepository.IsExist(b => b.Id == entity.BrandId && !b.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this brand is not exist"
            };
            else if (!await _unitOfWork.CategoryRepository.IsExist(c => c.Id == entity.CategoryId && !c.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this category is not exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.SellerId && !u.IsDeleted && u.IsActive && u.IsSeller)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "this user is not exist"
            };
            else if (!entity.ProductTags.Any(pt => _unitOfWork.TagRepository.IsExist(t => t.Id == pt.TagId && !t.IsDeleted).Result)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "tag is not exist"
            };
            foreach (var tag in entity.ProductTags)
            {
                await _unitOfWork.ProductTagRepository.Create(tag);
            }
            foreach (var image in entity.ProductImages)
            {
                await _unitOfWork.ProductImageRepository.Create(image);
            }
            entity.ProductCode = Guid.NewGuid().ToString().Substring(0, 6);
            entity.StarsCount = 5;
            await _unitOfWork.ProductRepository.Create(entity);
            await _unitOfWork.Complate();

            string link = $"{_privateUrl}/{entity.Id}";
            string imageUrl = entity.ProductImages.FirstOrDefault().ImageUrl;
            var cid = Guid.NewGuid().ToString(); // Generate a unique Content ID
            string emailMessageBody = $"<a style=' color: black;text-decoration: none;' href={link}>" +
                $"<img src='cid:{cid}' alt='Product Image' style='max-width:200px;height:200px'>" +
                $"<p class='text-success'>We Have a New Product. Do you want to see it: {entity.Name}?</p>" +
                $"<p class='text-warning'>Price: {entity.Price}$</p>" +
                $"</a>";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailMessageBody, null, MediaTypeNames.Text.Html);

            using (WebClient webClient = new WebClient())
            {
                byte[] imageData = webClient.DownloadData(imageUrl);
                LinkedResource linkedImage = new LinkedResource(new MemoryStream(imageData), MediaTypeNames.Image.Jpeg);
                linkedImage.ContentId = cid;
                htmlView.LinkedResources.Add(linkedImage);
            }

            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{entity.Name} successfully created"
            };
        }

        public async Task<ResponseObj> Delete(string id)
        {
            if (!await IsExist(p => p.Id == id && !p.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status404NotFound,
                ResponseMessage = "Product is not exist"
            };
            Product product = await GetEntity(p => p.Id == id, "Wishlists", "Baskets");
            product.IsDeleted = true;
            product.DeletedAt = DateTime.Now;
            foreach (var wishList in product.Wishlists)
            {
                wishList.IsDeleted = true;
                wishList.DeletedAt = DateTime.Now;
                await _unitOfWork.WishlistRepository.Update(wishList);
            }
            foreach (var basket in product.Baskets)
            {
                await _unitOfWork.BasketRepository.Delete(basket);
            }
            await _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Complate();
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{product.Name} successfully deleted"
            };
        }

        public async Task<List<Product>> GetAll(Expression<Func<Product, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.ProductRepository.GetAll(predicate, includes);
        }

        public async Task<Product> GetEntity(Expression<Func<Product, bool>> predicate = null, params string[] includes)
        {
            return await _unitOfWork.ProductRepository.GetEntity(predicate, includes);
        }

        public async Task<bool> IsExist(Expression<Func<Product, bool>> predicate = null)
        {
            return await _unitOfWork.ProductRepository.IsExist(predicate);
        }
        public async Task<int> SaveChanges()
        {
            return await _unitOfWork.Complate();
        }
        public async Task<ResponseObj> Update(Product entity)
        {
            if (!await _unitOfWork.BrandRepository.IsExist(b => b.Id == entity.BrandId && !b.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "this brand is not exist"
            };
            else if (!await _unitOfWork.CategoryRepository.IsExist(c => c.Id == entity.CategoryId && !c.IsDeleted)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "this category is not exist"
            };
            else if (!await _userManager.Users.AnyAsync(u => u.Id == entity.SellerId && !u.IsDeleted && u.IsActive && u.IsSeller)) return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status400BadRequest,
                ResponseMessage = "this user is not exist"
            };
            else if (entity.ProductTags.Count > 0)
            {
                if (!entity.ProductTags.Any(pt => _unitOfWork.TagRepository.IsExist(t => t.Id == pt.TagId && !t.IsDeleted).Result)) return new ResponseObj
                {
                    StatusCode = (int)StatusCodes.Status404NotFound,
                    ResponseMessage = "tag is not exist"
                };
            }

            if (entity.ProductTags.Count > 0)
            {
                List<ProductTag> productTags = await _unitOfWork.ProductTagRepository.GetAll(pt => pt.ProductId == entity.Id);
                foreach (var productTag in productTags)
                {
                    await _unitOfWork.ProductTagRepository.Delete(productTag);
                }
                foreach (var tag in entity.ProductTags.FindAll(pt => pt.Product == null))
                {
                    await _unitOfWork.ProductTagRepository.Create(tag);
                }
            }
            if (entity.ProductImages.Count > 0)
            {
                List<ProductImage> productImages = await _unitOfWork.ProductImageRepository.GetAll(pi => pi.ProductId == entity.Id);
                foreach (var productImage in productImages)
                {
                    await _unitOfWork.ProductImageRepository.Delete(productImage);
                }
                foreach (var image in entity.ProductImages.FindAll(pi => pi.Product == null))
                {
                    await _unitOfWork.ProductImageRepository.Create(image);
                }
            }
            entity.UpdatedAt = DateTime.Now;
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.ProductRepository.Update(entity);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{entity.Name} successfully updated"
            };
        }

        public async Task<ResponseObj> UpdateAfterPayment(Product entity)
        {
            entity.UpdatedAt = DateTime.Now;
            if (entity.Count == 0)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
            }
            if (!entity.IsDeleted)
            {
                entity.DeletedAt = null;
            }
            await _unitOfWork.ProductRepository.Update(entity);
            return new ResponseObj
            {
                StatusCode = (int)StatusCodes.Status200OK,
                ResponseMessage = $"{entity.Name} successfully updated"
            };
        }
    }
}

