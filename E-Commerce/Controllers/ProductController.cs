using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ProductDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly string _privateUrl = "https://localhost:7052/";
        public ProductController(IProductService productService, IFileService fileService, IMapper mapper, IPhotoAccessor photoAccessor)
        {
            _productService = productService;
            _mapper = mapper;
            _fileService = fileService;
            _photoAccessor = photoAccessor;
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (createProductDto.Images.Any(pi => !_fileService.IsImage(pi))) return BadRequest("upload only image");
            else if (createProductDto.Images.Any(pi => !_fileService.IsLengthSuit(pi, 1000))) return BadRequest(" image length must be smaller than 1kb");
            Product product = _mapper.Map<Product>(createProductDto);
            product.Id = Guid.NewGuid().ToString();
            foreach (var image in createProductDto.Images)
            {
                var resoult = await _photoAccessor.AddPhoto(image);
                product.ProductImages.Add(new ProductImage
                {
                    ImageUrl = resoult.SecureUrl.ToString(),
                    PublicId = resoult.PublicId,
                    ProductId = product.Id
                });
            }
            foreach (var tag in createProductDto.TagIds)
            {
                product.ProductTags.Add(new ProductTag
                {
                    TagId = tag,
                    ProductId = product.Id,

                });
            }
            if (product.IsDonation)
            {
                product.Price = 0;
                product.Tax = 0;
                product.SalePercentage = 0;
            }
            ResponseObj responseObj = await _productService.Create(product);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                foreach (var image in product.ProductImages)
                {
                    await _photoAccessor.DeletePhoto(image.PublicId);
                }
            }
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("Admin/{id}")]
        public async Task<IActionResult> UpdateByAdmin(string id, UpdateProductDto updateProductDto)
        {
            if (id == null || id != updateProductDto.Id) return BadRequest();
            else if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _productService.IsExist(p => p.Id == id)) return NotFound();
            Product product = await _productService.GetEntity(p => p.Id == id);
            List<string> oldImagePublicId = new();

            if (updateProductDto.TagIds != null)
            {
                product = await _productService.GetEntity(p => p.Id == id, "ProductTags.Tag");
            }
            if (updateProductDto.Images != null)
            {
                product = await _productService.GetEntity(p => p.Id == id, "ProductImages");
            }
            if (updateProductDto.Images != null && updateProductDto.TagIds != null)
            {
                product = await _productService.GetEntity(p => p.Id == id, "ProductImages", "ProductTags.Tag");
            }
            if (updateProductDto.IsVIP && !product.IsVIP)
            {
                product.VipDate = DateTime.Now;
            }
            if (!updateProductDto.IsVIP && product.IsVIP)
            {
                product.VipDate = null;
            }
            if (updateProductDto.IsAccepted && !product.IsAccepted)
            {
                product.AcceptedDate = DateTime.Now;
            }
            if (!updateProductDto.IsAccepted && product.IsAccepted)
            {
                product.AcceptedDate = null;
            }
            if (updateProductDto.TagIds != null)
            {
                foreach (var tag in updateProductDto.TagIds)
                {
                    product.ProductTags.Add(new ProductTag
                    {
                        Id = Guid.NewGuid().ToString(),
                        TagId = tag,
                        ProductId = product.Id,

                    });
                }
            }
            foreach (var image in product.ProductImages)
            {
                oldImagePublicId.Add(image.PublicId);
            }
            if (updateProductDto.Images != null)
            {
                if (updateProductDto.Images.Any(pi => !_fileService.IsImage(pi))) return BadRequest("upload only image");
                else if (updateProductDto.Images.Any(pi => !_fileService.IsLengthSuit(pi, 1000))) return BadRequest(" image length must be smaller than 1kb");
                foreach (var image in updateProductDto.Images)
                {
                    var resoult = await _photoAccessor.AddPhoto(image);
                    product.ProductImages.Add(new ProductImage
                    {
                        Id = Guid.NewGuid().ToString(),
                        ImageUrl = resoult.SecureUrl.ToString(),
                        PublicId = resoult.PublicId,
                        ProductId = product.Id
                    });
                }
            }
            _mapper.Map(updateProductDto, product);
            if (product.IsDonation)
            {
                product.Price = 0;
                product.Tax = 0;
                product.SalePercentage = 0;
            }
            ResponseObj responseObj = await _productService.Update(product);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                if (updateProductDto.Images != null)
                {
                    foreach (var image in product.ProductImages)
                    {
                        await _photoAccessor.DeletePhoto(image.PublicId);

                    }
                }
            }
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            if (updateProductDto.Images != null && oldImagePublicId != null && oldImagePublicId.Count > 0)
            {
                foreach (var publicId in oldImagePublicId)
                {
                    await _photoAccessor.DeletePhoto(publicId);

                }
            }
            await _productService.SaveChanges();
            return Ok(responseObj);
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateProductDto updateProductDto)
        {
            if (id == null || id != updateProductDto.Id) return BadRequest();
            else if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _productService.IsExist(p => p.Id == id)) return NotFound();
            Product product = await _productService.GetEntity(p => p.Id == id);
            List<string> oldImagePublicId = new();

            if (updateProductDto.TagIds != null)
            {
                product = await _productService.GetEntity(p => p.Id == id, "ProductTags.Tag");
            }
            if (updateProductDto.Images != null)
            {
                product = await _productService.GetEntity(p => p.Id == id, "ProductImages");
            }
            if (updateProductDto.Images != null && updateProductDto.TagIds != null)
            {
                product = await _productService.GetEntity(p => p.Id == id, "ProductImages", "ProductTags.Tag");
            }
            if (updateProductDto.IsVIP && !product.IsVIP)
            {
                product.VipDate = DateTime.Now;
            }
            if (!updateProductDto.IsVIP && product.IsVIP)
            {
                product.VipDate = null;
            }
            if (updateProductDto.IsAccepted && !product.IsAccepted)
            {
                product.AcceptedDate = DateTime.Now;
            }
            if (!updateProductDto.IsAccepted && product.IsAccepted)
            {
                product.AcceptedDate = null;
            }
            if (product.IsAccepted)
            {
                updateProductDto.IsAccepted = true;
            }
            if (product.IsVIP)
            {
                updateProductDto.IsAccepted = false;
            }
            if (product.VipDegre > 0)
            {
                updateProductDto.VipDegre = product.VipDegre;
            }
            if (updateProductDto.TagIds != null)
            {
                foreach (var tag in updateProductDto.TagIds)
                {
                    product.ProductTags.Add(new ProductTag
                    {
                        Id = Guid.NewGuid().ToString(),
                        TagId = tag,
                        ProductId = product.Id,

                    });
                }
            }
            foreach (var image in product.ProductImages)
            {
                oldImagePublicId.Add(image.PublicId);
            }
            if (updateProductDto.Images != null)
            {
                if (updateProductDto.Images.Any(pi => !_fileService.IsImage(pi))) return BadRequest("upload only image");
                else if (updateProductDto.Images.Any(pi => !_fileService.IsLengthSuit(pi, 1000))) return BadRequest(" image length must be smaller than 1kb");
                foreach (var image in updateProductDto.Images)
                {
                    var resoult = await _photoAccessor.AddPhoto(image);
                    product.ProductImages.Add(new ProductImage
                    {
                        Id = Guid.NewGuid().ToString(),
                        ImageUrl = resoult.SecureUrl.ToString(),
                        PublicId = resoult.PublicId,
                        ProductId = product.Id
                    });
                }
            }
            _mapper.Map(updateProductDto, product);
            ResponseObj responseObj = await _productService.Update(product);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                if (updateProductDto.Images != null)
                {
                    foreach (var image in product.ProductImages)
                    {
                        await _photoAccessor.DeletePhoto(image.PublicId);

                    }
                }
            }
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            if (updateProductDto.Images != null && oldImagePublicId != null && oldImagePublicId.Count > 0)
            {
                foreach (var publicId in oldImagePublicId)
                {
                    await _photoAccessor.DeletePhoto(publicId);

                }
            }
            await _productService.SaveChanges();
            return Ok(responseObj);

        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _productService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _productService.GetAll(p => !p.IsDeleted && p.IsAccepted, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            foreach (var product in products)
            {
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
            }
            return Ok(_mapper.Map<List<GetProductDto>>(products));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetProductByAdminDto>>(await _productService.GetAll(null, "Category", "Brand", "ProductImages", "Seller", "ProductComments", "Wishlists", "Baskets")));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest();
            Product product = await _productService.GetEntity(p => p.Id == id && !p.IsDeleted && p.IsAccepted, "Category", "Brand", "Seller");
            if (product == null)
            {
                return NotFound();
            }
            product.ReviewCount += 1;
            var responseObj = await _productService.Update(product);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            await _productService.SaveChanges();
            product = await _productService.GetEntity(p => p.Id == id && !p.IsDeleted, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments.AppUser", "Wishlists");
            product.ProductComments = product.ProductComments.OrderByDescending(c => c.CreatedAt).ToList().FindAll(c => !c.IsDeleted);
            product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);

            return Ok(_mapper.Map<GetProductDetailDto>(product));
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            Product product = await _productService.GetEntity(p => p.Id == id, "Category", "Brand", "ProductImages", "Seller", "ProductComments", "Wishlists", "Baskets");
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetProductByAdminDto>(product));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Statistics")]
        public async Task<IActionResult> Statistics()
        {
            var data = await _productService.GetAll();
            DateTime thisDay = DateTime.Now.AddDays(-1);
            DateTime thisWeek = DateTime.Now.AddDays(-7);
            DateTime thisMonth = DateTime.Now.AddDays(-30);
            ProductStatisticsDto productStatisticsDto = new();
            productStatisticsDto.LastDayCreatedCount = data.Where(p => p.CreatedAt > thisDay).ToList().Count();
            productStatisticsDto.LastDayDeletedCount = data.Where(p => p.DeletedAt > thisDay).ToList().Count();
            productStatisticsDto.LastDayUpdatedCount = data.Where(p => p.UpdatedAt > thisDay).ToList().Count();

            productStatisticsDto.LastWeekCreatedCount = data.Where(p => p.CreatedAt > thisWeek).ToList().Count();
            productStatisticsDto.LastWeekDeletedCount = data.Where(p => p.DeletedAt > thisWeek).ToList().Count();
            productStatisticsDto.LastWeekUpdatedCount = data.Where(p => p.UpdatedAt > thisWeek).ToList().Count();

            productStatisticsDto.LastMonthCreatedCount = data.Where(p => p.CreatedAt > thisMonth).ToList().Count();
            productStatisticsDto.LastMonthDeletedCount = data.Where(p => p.DeletedAt > thisMonth).ToList().Count();
            productStatisticsDto.LastMonthUpdatedCount = data.Where(p => p.UpdatedAt > thisMonth).ToList().Count();

            return Ok(productStatisticsDto);
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Product, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetProductByAdminDto>>(
               await _productService.GetAll(filter, "Category", "Brand", "ProductImages", "Seller", "Wishlists", "Baskets")
           ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            if (name == null || name.Trim() == "") return BadRequest("something went wrong");
            return Ok(_mapper.Map<List<GetProductByAdminDto>>(await _productService.GetAll(p => p.Name.ToLower().Contains(name.ToLower()), "Category", "Brand", "ProductImages", "Seller", "ProductComments", "Wishlists", "Baskets")));

        }
        [HttpGet("UserSearch")]
        public async Task<IActionResult> UserSearch(string name)
        {
            if (name == null || name.Trim() == "") return BadRequest("something went wrong");
            var data = await _productService.GetAll(p => p.Name.ToLower().Contains(name.ToLower()) && !p.IsDeleted && p.IsAccepted, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            foreach (var product in data)
            {
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
            }
            return Ok(_mapper.Map<List<GetProductDto>>(data));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Product> products = await _productService.GetAll(null, "Category", "Brand", "ProductImages", "Seller", "ProductComments", "Wishlists", "Baskets");
            var data = _mapper.Map<List<GetProductByAdminDto>>(products.OrderByDescending(p => p.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = products.Count, data, pendingCount = (await _productService.GetAll(p => !p.IsAccepted)).Count });
        }
        [HttpGet("UserPaggination")]
        public async Task<IActionResult> UserPaginnation(int skip = 0, int take = 4)
        {
            List<Product> products = await _productService.GetAll(p => !p.IsDeleted && p.IsAccepted, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            foreach (var product in products)
            {
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
            }
            var data = _mapper.Map<List<GetProductDto>>(products.OrderByDescending(p => p.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = products.Count, data });
        }
        [HttpGet("FilterByProductForDifferentOptions")]
        public async Task<IActionResult> FilterByProductForDifferentOptions(int skip = 0, int take = 4, int status = 1)
        {
            List<Product> products = await _productService.GetAll(p => !p.IsDeleted && p.IsAccepted, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            foreach (var product in products)
            {
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
            }
            var data = _mapper.Map<List<GetProductDto>>(status == (int)ProductFilterStatus.FilterByLatestItems ? products.OrderBy(p => p.CreatedAt).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByNewItems ? products.OrderByDescending(p => p.CreatedAt).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByBestSelling ? products.OrderByDescending(p => p.ReviewCount).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByBestRating ? products.OrderByDescending(p => p.StarsCount).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByHighestPrice ? products.OrderByDescending(p => p.Price).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByLowestPrice ? products.OrderBy(p => p.Price).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByLowestPrice ? products.OrderBy(p => p.Price).Skip(skip).Take(take)
                : status == (int)ProductFilterStatus.FilterByVIP ? (await _productService.GetAll(p => !p.IsDeleted && p.IsAccepted && p.IsVIP, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists")).OrderByDescending(p => p.CreatedAt).Skip(skip).Take(take)
                : products.OrderBy(p => p.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = products.Count, data });
        }
        [HttpGet("GetHighWatching")]
        public async Task<IActionResult> GetHighWatching(string? id, int skip = 0, int take = 4)
        {
            List<Product> products = await _productService.GetAll(p => id != null ? !p.IsAccepted && p.IsDeleted && p.Id != id : !p.IsDeleted && p.IsAccepted, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            foreach (var product in products)
            {
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
            }
            var data = _mapper.Map<List<GetProductDto>>(products.OrderByDescending(p => p.ReviewCount).Skip(skip).Take(take));
            return Ok(data);
        }
        [HttpPost("CheckOut")]
        public async Task<IActionResult> CheckOut(string productId, int degre)
        {
            Product product = await _productService.GetEntity(p => p.Id == productId && !p.IsDeleted && p.IsAccepted && !p.IsVIP, "Seller", "ProductImages");
            if (product == null) return BadRequest("invalid product");
            var domain = _privateUrl;
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"ConfirmPayment",
                CancelUrl = domain + "login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = product.Seller.Email
            };
            var totalPrice = degre == 1 ? 2 : degre == 3 ? 5 : degre == 7 ? 10 : 15;
            totalPrice *= 100;
            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)totalPrice,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Name.ToString(),
                        Images = product.ProductImages.Select(i => i.ImageUrl).ToList()


                    }
                },
                Quantity = 1

            };
            options.LineItems.Add(sessionListItem);
            var service = new SessionService();
            Session session = service.Create(options);
            return Ok(new { Url = session.Url, SesionId = session.Id, ProductId = product.Id, Degre = degre });
        }
        [HttpPost("ConfirmPayment")]
        public async Task<IActionResult> ConfirmPayment(string productId, int degre, string sesionId)
        {
            var service = new SessionService();
            Session session = service.Get(sesionId);
            if (session.PaymentStatus != "paid")
            {
                return BadRequest("happen payment problem");
            }
            Product product = await _productService.GetEntity(p => p.Id == productId && p.IsAccepted && !p.IsVIP && !p.IsDeleted);
            if (product == null) return BadRequest("invalid product");
            product.VipDegre = degre;
            product.IsVIP = true;
            product.VipDate = DateTime.Now;
            ResponseObj responseObj = await _productService.UpdateAfterPayment(product);
            await _productService.SaveChanges();
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);

            return Ok(responseObj);
        }
        [HttpGet("GetDonations")]
        public async Task<IActionResult> GetDonations(int? take)
        {
            List<Product> products = await _productService.GetAll(p => !p.IsDeleted && p.IsAccepted && p.IsDonation, "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            if (take != null)
            {
                int count = (int)take;
                products = products.Take(count).ToList();
            }
            foreach (var product in products)
            {
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
            }
            var data = _mapper.Map<List<GetProductDto>>(products);
            return Ok(data);
        }

        [HttpGet("GetForCategory")]
        public async Task<IActionResult> GetForCategory(string categoryName, int? take)
        {
            List<Product> products = await _productService.GetAll(p => !p.IsDeleted && p.IsAccepted && p.Category.Name.ToLower() == categoryName.ToLower(), "Category", "Brand", "ProductImages", "Seller", "ProductTags.Tag", "ProductComments", "Wishlists");
            if (take != null)
            {
                int count = (int)take;
                products = products.Take(count).ToList();
            }
            foreach (var product in products)
            {
                product.ProductComments = product.ProductComments.FindAll(c => !c.IsDeleted);
                product.Wishlists = product.Wishlists.FindAll(w => !w.IsDeleted);
            }
            var data = _mapper.Map<List<GetProductDto>>(products);
            return Ok(data);
        }
    }
}

