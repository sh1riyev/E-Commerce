using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using E_Commerce.Business.DTOs.CheckDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CheckOutController : Controller
    {
        private readonly ICheckService _checkService;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompaignsService _compaignsService;
        private readonly IAdressService _adressService;
        private readonly IMapper _mapper;
        private readonly ISendEmail _sendEmail;
        private readonly string _publicUrl = "https://rewear.site/";
        private readonly string _privateUrl = "http://localhost:8080/";
        public CheckOutController(IAdressService adressService,ICompaignsService compaignsService,UserManager<AppUser>userManager,ICheckService checkService,IBasketService basketService,IProductService productService,IMapper mapper,ISendEmail sendEmail)
        {
            _adressService = adressService;
            _compaignsService = compaignsService;
            _userManager = userManager;
            _basketService = basketService;
            _checkService = checkService;
            _productService = productService;
            _mapper = mapper;
            _sendEmail = sendEmail;
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut([FromBody]CheckOutDto checkOutDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _userManager.Users.AnyAsync(u => u.Id == checkOutDto.UserId && !u.IsDeleted && u.IsActive))
                return BadRequest("User is not exist");
            else if (!await _adressService.IsExist(a => a.Id == checkOutDto.AdressId && !a.IsDeleted&&a.UserId==checkOutDto.UserId))
                return BadRequest("adress is not exist");
            else if (checkOutDto.SaleCode!=null)
            {
                 if (!await _compaignsService.IsExist(c => !c.IsDeleted && c.ExpirationDate > DateTime.Now && c.Info.ToLower() == checkOutDto.SaleCode.ToLower()))
                {
                    return BadRequest("promocode is not valid");
                }
            }
            List<Basket> baskets = await _basketService.GetAll(b=>!b.IsDeleted&&b.UserId==checkOutDto.UserId,"Product.ProductImages", "AppUser");
            foreach (var basket in baskets)
            {
                if (basket.Product.Count < basket.Count)
                {
                    return BadRequest("product is out of stock");
                }
                else if (basket.Product.SellerId==checkOutDto.UserId)
                {
                    return BadRequest($"{basket.Product.Name} is belong to your stock");
                }
            }
            if (baskets.Count == 0)
            {
                return BadRequest("dont have any busket");
            }
            Address adress = await _adressService.GetEntity(a => a.Id == checkOutDto.AdressId, "City");
            AppUser user = await _userManager.FindByIdAsync(checkOutDto.UserId);
            if (user.PhoneNumber==null)
            {
                return BadRequest("Please Edit your phone number");
            }
            var domain = _privateUrl;
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"ConfirmBasket",
                CancelUrl = domain + "login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = user.Email,



            };
            var totalPrice = CalculateSubTax(baskets) + CalculateSubtotal(baskets);
            totalPrice += adress.City.DeliverPrice;
            double sale = 0.0;
            if (checkOutDto.SaleCode!=null)
            {
                var promocode = await _compaignsService.GetEntity(c => c.Info.ToLower() == checkOutDto.SaleCode.ToLower());
                if (promocode != null)
                {
                    sale = promocode.Sale;
                    totalPrice -= totalPrice * promocode.Sale / 100;
                }

            }
            totalPrice *= 100;
            foreach (var basket1 in baskets)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)((totalPrice)/(baskets.Count*basket1.Count)),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = basket1.Product.Name.ToString(),
                            Images = basket1.Product.ProductImages.Select(i => i.ImageUrl).ToList()


            }
        },
                    Quantity = basket1.Count

                };
                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            return Ok(new { Url=session.Url,AdressId=checkOutDto.AdressId,SesionId=session.Id,SaleCode=checkOutDto.SaleCode });
        }
        public static double CalculateSubtotal(List<Basket> basket)
        {
            if (basket == null || basket.Count == 0)
            {
                return 0;
            }

            return basket.Sum(item =>
            {
                double itemPrice = item.Product.Price - (item.Product.Price * item.Product.SalePercentage / 100);
                int itemCount = item.Count;
                return itemPrice * itemCount;
            });
        }

        public static double CalculateSubTax(List<Basket> basket)
        {
            if (basket == null || basket.Count == 0)
            {
                return 0;
            }

            return basket.Sum(item =>
            {
                double itemTax = item.Product.Tax;
                return itemTax;
            });
        }
        [HttpPost("ConfirmBasket")]
        public async Task<IActionResult> ConfirmBasket([FromBody]ConfirmBasketDto confirmBasketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _userManager.Users.AnyAsync(u => u.Id == confirmBasketDto.UserId && !u.IsDeleted && u.IsActive))
                return BadRequest("User is not exist");
            else if (!await _adressService.IsExist(a => a.Id == confirmBasketDto.AdressId && !a.IsDeleted && a.UserId == confirmBasketDto.UserId))
                return BadRequest("adress is not exist");
            else if (confirmBasketDto.SesionId==null)
            {
                return BadRequest("something went wrong");
            }
            Address adress = await _adressService.GetEntity(a => a.Id == confirmBasketDto.AdressId, "City");
            var service = new SessionService();
            Session session = service.Get(confirmBasketDto.SesionId);
            if (session.PaymentStatus != "paid")
            {
                return BadRequest("happen payment problem");
            }
            List<Basket> baskets = await _basketService.GetAll(b => !b.IsDeleted && b.UserId == confirmBasketDto.UserId, "Product.ProductImages", "AppUser");
            Check check = new();
            var totalPrice = CalculateSubTax(baskets) + CalculateSubtotal(baskets);
            totalPrice += adress.City.DeliverPrice;
            double sale = 0.0;
            if (confirmBasketDto.SaleCode != null)
            {
                var promocode = await _compaignsService.GetEntity(c => !c.IsDeleted && c.ExpirationDate > DateTime.Now && c.Info.ToLower() == confirmBasketDto.SaleCode.ToLower());
                if (promocode != null)
                {
                    sale = promocode.Sale;
                    totalPrice -= totalPrice * promocode.Sale / 100;
                    check.Promocode = promocode.Info;
                }

            }
            check.Id = Guid.NewGuid().ToString();
            check.AdressId = confirmBasketDto.AdressId;
            check.Sale = sale;
            check.UserId = confirmBasketDto.UserId;
            check.TotalAmmount = totalPrice;



            foreach (var basket in baskets)
            {

                Product existProduct = await _productService.GetEntity(p => p.Id == basket.ProductId);
                check.CheckProducts.Add(new CheckProduct { ProductId = basket.ProductId, CheckId = check.Id, Price = basket.Product.Price-(basket.Product.Price*basket.Product.SalePercentage/100)+basket.Product.Tax, ProductCount = basket.Count });
                existProduct.Count -= basket.Count;
                await _productService.UpdateAfterPayment(existProduct);
                await _basketService.Delete(basket.Id);
            }
            await _basketService.SaveChanges();
            ResponseObj responseObj = await _checkService.Create(check);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);

            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("something went wrong");
                }
                ResponseObj responseObj = await _checkService.Delete(id);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Check> checks = await _checkService.GetAll(c => !c.IsDeleted, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser");
                List<GetCheckDto> getCities = _mapper.Map<List<GetCheckDto>>(checks);
                return Ok(getCities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("something went wrong");
                }
                else if (!await _checkService.IsExist(c => c.Id == id && !c.IsDeleted))
                {
                    return NotFound("Check not founded");
                }
                Check check = await _checkService.GetEntity(c => !c.IsDeleted, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser");
                return Ok(_mapper.Map<GetCheckDto>(check));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            try
            {
                DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
                Expression<Func<Check, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetCheckByAdminDto>>(
                   await _checkService.GetAll(filter, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser")
               ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string userName)
        {
            try
            {
                if (userName == null) return BadRequest("userName is required");
                return Ok(_mapper.Map<List<GetCheckByAdminDto>>(await _checkService.GetAll(c => c.AppUser.UserName.ToLower().Contains(userName.ToLower()), "Adress", "CheckProducts.Product.ProductImages", "AppUser")));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            try
            {
                List<Check> checks = await _checkService.GetAll(null, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser");
                var data = _mapper.Map<List<GetCheckByAdminDto>>(checks.OrderBy(c => c.CreatedAt).Skip(skip).Take(take));
                return Ok(new { size = checks.Count, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                else if (!await _checkService.IsExist(c => c.Id == id))
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetCheckByAdminDto>(await _checkService.GetEntity(c => c.Id == id, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser")));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            try
            {
                return Ok(_mapper.Map<List<GetCheckByAdminDto>>(await _checkService.GetAll(null, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser")));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Statistics")]
        public async Task<IActionResult> Statistics()
        {
            try
            {
                var data= await _checkService.GetAll();
                CheckStatisticsDto checkStatistics = new();
                checkStatistics.ProsessingCount = data.Where(c => c.Status == 1).ToList().Count;
                checkStatistics.PreparingCount = data.Where(c => c.Status == 2).ToList().Count;
                checkStatistics.ShipedCount = data.Where(c => c.Status == 3).ToList().Count;
                checkStatistics.DeliveredCount = data.Where(c => c.Status == 4).ToList().Count;
                checkStatistics.DeletedCount = data.Where(c => c.IsDeleted).ToList().Count;
                return Ok(checkStatistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCheckDto updateCheckDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (id == null || id != updateCheckDto.Id) return BadRequest("something went wrong");
                else if (!await _checkService.IsExist(c => c.Id == id))
                {
                    return NotFound("Check is not exist");
                }
                Check check = await _checkService.GetEntity(c => c.Id == id);
                _mapper.Map(updateCheckDto, check);
                ResponseObj responseObj = await _checkService.Update(check);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

