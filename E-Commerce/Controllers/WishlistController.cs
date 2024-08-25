using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.WishlistDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public WishlistController(IWishlistService wishlistService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _wishlistService = wishlistService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWishlistDto createWishlistDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Wishlist wishlist = _mapper.Map<Wishlist>(createWishlistDto);
            ResponseObj responseObj = await _wishlistService.Create(wishlist);
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
            ResponseObj responseObj = await _wishlistService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            await _wishlistService.SaveChanges();
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
            AppUser appUser = await _userManager.Users.Include(u => u.Wishlists).FirstOrDefaultAsync(u => u.Id == userId);
            if (appUser.Wishlists.Count == 0)
            {
                return BadRequest("User dont have any wishlist");
            }
            foreach (var wishList in appUser.Wishlists)
            {

                if (!wishList.IsDeleted)
                {
                    ResponseObj responseObj = await _wishlistService.Delete(wishList.Id);
                    if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                    if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                }
            }
            await _wishlistService.SaveChanges();
            return Ok("User All WishList Successfully removed");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Wishlist> wishlists = await _wishlistService.GetAll(w => !w.IsDeleted, "Product", "AppUser");
            List<GetWishlistDto> getWishlistDtos = _mapper.Map<List<GetWishlistDto>>(wishlists);
            return Ok(getWishlistDtos);
        }
        [HttpGet("IsExist")]
        public async Task<IActionResult> IsExist(string productId, string userId)
        {
            return Ok(await _wishlistService.IsExist(w => w.ProductId == productId && w.UserId == userId && !w.IsDeleted));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(string id)
        {
            if (id == null)
            {
                return BadRequest("something went wrong");
            }
            else if (!await _wishlistService.IsExist(w => w.Id == id && !w.IsDeleted))
            {
                return NotFound("wishlist not found");
            }
            Wishlist wishlist = await _wishlistService.GetEntity(w => w.Id == id, "Product", "AppUser");
            return Ok(_mapper.Map<GetWishlistDto>(wishlist));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Wishlist, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetWishlistByAdmin>>(
                await _wishlistService.GetAll(filter, "Product", "AppUser")
            ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string productName)
        {
            if (productName == null && productName.Trim() == "") return BadRequest("Name is required");
            return Ok(_mapper.Map<List<GetWishlistByAdmin>>(await _wishlistService.GetAll(t => t.Product.Name.ToLower().Contains(productName.ToLower()), "Product", "AppUser")));
        }


        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Wishlist> wishlists = await _wishlistService.GetAll(null, "Product", "AppUser");
            var data = _mapper.Map<List<GetWishlistByAdmin>>(wishlists.OrderBy(t => t.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = wishlists.Count, data });
        }

        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _wishlistService.IsExist(w => w.Id == id))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetWishlistByAdmin>(await _wishlistService.GetEntity(w => w.Id == id, "Product", "AppUser")));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetWishlistByAdmin>>(await _wishlistService.GetAll(null, "Product", "AppUser")));
        }
    }
}

