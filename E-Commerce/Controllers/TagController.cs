using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.TagDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public TagController(IMapper mapper, ITagService tagService)
        {
            _tagService = tagService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDto createTagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Tag tag = _mapper.Map<Tag>(createTagDto);
            ResponseObj responseObj = await _tagService.Create(tag);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            return Ok(responseObj);
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest("something went wrong");
            }
            ResponseObj responseObj = await _tagService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Tag> tags = await _tagService.GetAll(t => !t.IsDeleted);
            List<GetTagDto> getTagDtos = _mapper.Map<List<GetTagDto>>(tags);
            return Ok(getTagDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(string id)
        {
            if (id == null)
            {
                return BadRequest("something went wrong");
            }
            else if (!await _tagService.IsExist(t => t.Id == id && !t.IsDeleted))
            {
                return NotFound("tag not found");
            }
            Tag tag = await _tagService.GetEntity(t => t.Id == id);
            return Ok(_mapper.Map<GetTagDto>(tag));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Tag, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetTagByAdminDto>>(
                await _tagService.GetAll(filter)
            ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            if (name == null) return BadRequest("Name is required");
            return Ok(_mapper.Map<List<GetTagByAdminDto>>(await _tagService.GetAll(t => t.Name.ToLower().Contains(name.ToLower()))));
        }


        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Tag> tags = await _tagService.GetAll();
            var data = _mapper.Map<List<GetTagByAdminDto>>(tags.OrderBy(t => t.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = tags.Count, data });
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _tagService.IsExist(t => t.Id == id))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetTagByAdminDto>(await _tagService.GetEntity(c => c.Id == id)));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetTagByAdminDto>>(await _tagService.GetAll()));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateTagDto updateTagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (id == null || id != updateTagDto.Id) return BadRequest("something went wrong");
            else if (!await _tagService.IsExist(s => s.Id == id))
            {
                return NotFound("Tag isnot exist");
            }
            Tag tag = await _tagService.GetEntity(t => t.Id == id);
            _mapper.Map(updateTagDto, tag);
            ResponseObj responseObj = await _tagService.Update(tag);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
    }
}

