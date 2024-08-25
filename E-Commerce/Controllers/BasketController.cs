using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.BasketDto;
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
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public BasketController(IBasketService basketService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create(BasketDto basketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Basket basket = _mapper.Map<Basket>(basketDto);
            ResponseObj responseObj = await _basketService.Create(basket);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            return Ok(responseObj);
        }
        [HttpPost("IncreaseCount")]
        public async Task<IActionResult> IncreaseCount(BasketDto basketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _basketService.IsExist(b => b.ProductId == basketDto.ProductId && !b.IsDeleted && b.UserId == basketDto.UserId))
            {
                return BadRequest("Basket is not exist");
            }
            Basket basket = await _basketService.GetEntity(b => b.ProductId == basketDto.ProductId && !b.IsDeleted && b.UserId == basketDto.UserId);
            ResponseObj responseObj = await _basketService.IncreaseCount(basket);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            return Ok(responseObj);
        }
        [HttpPost("DecreaseCount")]
        public async Task<IActionResult> DecreaseCount(BasketDto basketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _basketService.IsExist(b => b.ProductId == basketDto.ProductId && !b.IsDeleted && b.UserId == basketDto.UserId))
            {
                return BadRequest("Basket is not exist");
            }
            Basket basket = await _basketService.GetEntity(b => b.ProductId == basketDto.ProductId && !b.IsDeleted && b.UserId == basketDto.UserId);
            ResponseObj responseObj = await _basketService.DecreaseCount(basket);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest("something went wrong");
            }
            ResponseObj responseObj = await _basketService.Delete(id);
            await _basketService.SaveChanges();
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("User/{userId}")]
        public async Task<IActionResult> UserDelete(string userId)
        {
            if (userId == null)
            {
                return BadRequest("something went wrong");
            }
            if (!await _userManager.Users.AnyAsync(u => u.Id == userId && !u.IsDeleted && u.IsActive)) return BadRequest("user is not exist");
            AppUser appUser = await _userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.Id == userId);
            if (appUser.Baskets.Count == 0)
            {
                return BadRequest("User dont have any basket");
            }
            foreach (var basket in appUser.Baskets)
            {

                if (!basket.IsDeleted)
                {
                    ResponseObj responseObj = await _basketService.Delete(basket.Id);
                    if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                    if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                }
            }
            await _basketService.SaveChanges();
            return Ok("User All Basket Successfully removed");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Basket> baskets = await _basketService.GetAll(b => !b.IsDeleted, "Product.ProductImages", "AppUser");
            List<GetBasketDto> getBasketDtos = _mapper.Map<List<GetBasketDto>>(baskets);
            return Ok(getBasketDtos);
        }
        [HttpGet("GetBasketCount")]
        public async Task<IActionResult> GetBasketCount(string productId, string userId)
        {
            if (!await _basketService.IsExist(b => b.ProductId == productId && b.UserId == userId && !b.IsDeleted))
            {
                return Ok(0);
            }
            Basket basket = await _basketService.GetEntity(b => b.ProductId == productId && b.UserId == userId);
            return Ok(basket.Count);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(string id)
        {
            if (id == null)
            {
                return BadRequest("something went wrong");
            }
            else if (!await _basketService.IsExist(b => b.Id == id && !b.IsDeleted))
            {
                return NotFound("wishlist not found");
            }
            Basket basket = await _basketService.GetEntity(b => b.Id == id, "Product.ProductImages", "AppUser");
            return Ok(_mapper.Map<GetBasketDto>(basket));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Basket, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetBasketByAdminDto>>(
               await _basketService.GetAll(filter, "Product.ProductImages", "AppUser")
           ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string productName)
        {
            if (productName == null && productName.Trim() == "") return BadRequest("Name is required");
            return Ok(_mapper.Map<List<GetBasketByAdminDto>>(await _basketService.GetAll(b => b.Product.Name.ToLower().Contains(productName.ToLower()), "Product.ProductImages", "AppUser")));

        }


        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Basket> baskets = await _basketService.GetAll(null, "Product.ProductImages", "AppUser");
            var data = _mapper.Map<List<GetBasketByAdminDto>>(baskets.OrderBy(b => b.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = baskets.Count, data });

        }

        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _basketService.IsExist(b => b.Id == id))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetBasketByAdminDto>(await _basketService.GetEntity(b => b.Id == id, "Product.ProductImages", "AppUser")));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetBasketByAdminDto>>(await _basketService.GetAll(null, "Product.ProductImages", "AppUser")));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBasketDto updateBasketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (id == null || id != updateBasketDto.Id) return BadRequest("something went wrong");
            else if (!await _basketService.IsExist(b => b.Id == id))
            {
                return NotFound("Wishlist is not exist");
            }
            Basket basket = await _basketService.GetEntity(b => b.Id == id);
            _mapper.Map(updateBasketDto, basket);
            ResponseObj responseObj = await _basketService.Update(basket);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

