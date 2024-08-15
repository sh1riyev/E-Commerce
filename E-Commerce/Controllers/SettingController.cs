using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.SettingDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;
        public SettingController(ISettingService settingService,IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult>Create(CreateSettingDto createSettingDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ResponseObj responseObj = await _settingService.Create(_mapper.Map<Setting>(createSettingDto));
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
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
                ResponseObj responseObj = await _settingService.Delete(id);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult>Update(string id,UpdateSettingDto updateSettingDto)
        {
            try
            {
                if (id == null || id != updateSettingDto.Id) return BadRequest();
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (!await _settingService.IsExist(s => s.Id == id)) return NotFound("not found");
                Setting setting = await _settingService.GetEntity(s => s.Id == id);
                _mapper.Map(updateSettingDto, setting);
                ResponseObj responseObj = await _settingService.Update(setting);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
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
                return Ok(_mapper.Map<List<GetSettingDto>>(await _settingService.GetAll(s => !s.IsDeleted)));
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
                return Ok(_mapper.Map<List<GetSettingByAdminDto>>(await _settingService.GetAll()));
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
                Setting setting = await _settingService.GetEntity(s => s.Id == id && !s.IsDeleted);
                if (setting == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetSettingDto>(setting));
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
                if (id == null) return BadRequest();
                Setting setting = await _settingService.GetEntity(s => s.Id == id);
                if (setting == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetSettingByAdminDto>(setting));
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
                Expression<Func<Setting, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetSettingByAdminDto>>(
                     await _settingService.GetAll(filter)
                 ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string key)
        {
            try
            {
                if (key == null) return BadRequest("key is required");
                return Ok(_mapper.Map<List<GetSettingByAdminDto>>(await _settingService.GetAll(s => s.Key.ToLower().Contains(key.ToLower()))));
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
                List<Setting> settings = await _settingService.GetAll();
                var data = _mapper.Map<List<GetSettingByAdminDto>>(settings.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
                return Ok(new { size = settings.Count,data});
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

