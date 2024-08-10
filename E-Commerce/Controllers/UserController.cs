using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.UserDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SupperAdmin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IUserService userService,RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAllUser(null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                return Ok(await _roleManager.Roles.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _userService.IsExist(u => u.Id == id)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id);
                if (getUserDto == null) return NotFound("user is not exist");
                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("SearchByUser")]
        public async Task<IActionResult> SearchByUser(string userName)
        {
            try
            {
                if (userName == null || userName.Trim() == "") return BadRequest("something went wrong");
                 return Ok(await _userService.GetAllUser(u => !u.IsDeleted && u.UserName.ToLower().Contains(userName.ToLower())));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetAllNonActiveSeller")]
        public async Task<IActionResult> GetAllNonActiveSeller()
        {
            try
            {
                List<GetUserDto> getUserDtos = await _userService.GetAllUser(u=>u.IsSeller&&!u.IsActive);
                return Ok(getUserDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetAllSeller")]
        public async Task<IActionResult> GetAllSeller()
        {
            try
            {
                List<GetUserDto> getUserDtos = await _userService.GetAllUser(u=>u.IsSeller);
                return Ok(getUserDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetAllAdminByUser")]
        public async Task<IActionResult> GetAllAdminByUser()
        {
            try
            {
                List<GetUserDto> getUserDtos = await _userService.GetAllUser(null);
                var data = getUserDtos.FindAll(u => u.Roles.Any(r => r == "Admin" || r == "SupperAdmin")).Select(u => new {u.ProfileImageUrl,u.FullName,u.UserName,Role= u.Roles.Any(r => r == "SupperAdmin") ?"SupperAdmin":"Admin" });
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetAllAdmin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            try
            {
                List<GetUserDto> getUserDtos = await _userService.GetAllUser(null);
                getUserDtos = getUserDtos.FindAll(u => u.Roles.Any(r=>r=="Admin"||r== "SupperAdmin"));
                return Ok(getUserDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            try
            {
                DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
                Expression<Func<AppUser, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.RemovedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(await _userService.GetAllUser(filter));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string userName)
        {
            try
            {
                if (userName == null || userName.Trim() == "") return BadRequest("something went wrong");
                return Ok(await _userService.GetAllUser(u => u.UserName.ToLower().Contains(userName.ToLower())));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            try
            {
                List<GetUserDto> getUserDtos = await _userService.GetAllUser(null);
                var data = getUserDtos.OrderBy(b => b.CreatedAt).Skip(skip).Take(take);
                return Ok(new {size=getUserDtos.Count,data, pendingCount = (await _userService.GetAllUser(u => u.IsSeller && !u.IsActive)).Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                ResponseObj responseObj = await _userService.Delete(id);
                if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>Update(string id, UpdateUserDto updateUserDto)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                ResponseObj responseObj = await _userService.Update(id,updateUserDto);
                if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

