using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.SubscribeDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;
        private readonly IMapper _mapper;
        public SubscribeController(ISubscribeService subscribeService, IMapper mapper)
        {
            _subscribeService = subscribeService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<GetSubscribeDto>>(await _subscribeService.GetAll(s => !s.IsDeleted)));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetSubscribeByAdminDto>>(await _subscribeService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest();
            Subscribe subscribe = await _subscribeService.GetEntity(s => s.Id == id && !s.IsDeleted);
            if (subscribe == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetSubscribeDto>(subscribe));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            Subscribe subscribe = await _subscribeService.GetEntity(s => s.Id == id);
            if (subscribe == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetSubscribeByAdminDto>(subscribe));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Subscribe, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetSubscribeByAdminDto>>(
               await _subscribeService.GetAll(filter)
           ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string email)
        {
            if (email == null) return BadRequest("email is required");
            return Ok(_mapper.Map<List<GetSubscribeByAdminDto>>(await _subscribeService.GetAll(s => s.Email.ToLower().Contains(email.ToLower()))));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Subscribe> subscribes = await _subscribeService.GetAll();
            var data = _mapper.Map<List<GetSubscribeByAdminDto>>(subscribes.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = subscribes.Count, data });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscribeDto createSubscribeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _subscribeService.Create(_mapper.Map<Subscribe>(createSubscribeDto));
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSubscribeDto updateSubscribeDto)
        {
            if (id == null || id != updateSubscribeDto.Id) return BadRequest();
            else if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _subscribeService.IsExist(s => s.Id == id)) return NotFound();
            Subscribe subscribe = await _subscribeService.GetEntity(s => s.Id == id);
            _mapper.Map(updateSubscribeDto, subscribe);
            ResponseObj responseObj = await _subscribeService.Update(subscribe);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }

        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _subscribeService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

