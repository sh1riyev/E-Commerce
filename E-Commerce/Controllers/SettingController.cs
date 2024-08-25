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
        public SettingController(ISettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSettingDto createSettingDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _settingService.Create(_mapper.Map<Setting>(createSettingDto));
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _settingService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSettingDto updateSettingDto)
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<GetSettingDto>>(await _settingService.GetAll(s => !s.IsDeleted)));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetSettingByAdminDto>>(await _settingService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest();
            Setting setting = await _settingService.GetEntity(s => s.Id == id && !s.IsDeleted);
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetSettingDto>(setting));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            Setting setting = await _settingService.GetEntity(s => s.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetSettingByAdminDto>(setting));
        }

        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Setting, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetSettingByAdminDto>>(
                 await _settingService.GetAll(filter)
             ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string key)
        {
            if (key == null) return BadRequest("key is required");
            return Ok(_mapper.Map<List<GetSettingByAdminDto>>(await _settingService.GetAll(s => s.Key.ToLower().Contains(key.ToLower()))));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Setting> settings = await _settingService.GetAll();
            var data = _mapper.Map<List<GetSettingByAdminDto>>(settings.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = settings.Count, data });
        }
    }
}

