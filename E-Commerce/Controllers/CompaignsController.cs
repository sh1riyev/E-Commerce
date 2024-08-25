using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.CompaignsDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class CompaignsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICompaignsService _compaignsService;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IFileService _fileService;
        public CompaignsController(ICompaignsService compaignsService, IMapper mapper, IPhotoAccessor photoAccessor, IFileService fileService)
        {
            _mapper = mapper;
            _photoAccessor = photoAccessor;
            _compaignsService = compaignsService;
            _fileService = fileService;
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCompaignsDto createCompaignsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_fileService.IsImage(createCompaignsDto.Image))
            {
                return BadRequest("you must upload only image");
            }
            else if (!_fileService.IsLengthSuit(createCompaignsDto.Image, 1000))
            {
                return BadRequest("Image size must be smaller than 1kb");
            }
            var imageResoult = await _photoAccessor.AddPhoto(createCompaignsDto.Image);
            Compaigns compaigns = _mapper.Map<Compaigns>(createCompaignsDto);
            compaigns.ImageUrl = imageResoult.SecureUrl.ToString();
            compaigns.PublicId = imageResoult.PublicId;
            var response = await _compaignsService.Create(compaigns);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                await _photoAccessor.DeletePhoto(imageResoult.PublicId);
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Compaigns> compaigns = await _compaignsService.GetAll(c => !c.IsDeleted);
            List<GetCompaignsDto> getCompaignsDtos = _mapper.Map<List<GetCompaignsDto>>(compaigns.OrderByDescending(c => c.CreatedAt));

            return Ok(getCompaignsDtos);
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var response = await _compaignsService.Delete(id);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _compaignsService.IsExist(c => c.Id == id && !c.IsDeleted))
            {
                return NotFound();
            }
            Compaigns compaigns = await _compaignsService.GetEntity(c => c.Id == id);
            return Ok(_mapper.Map<GetCompaignsDto>(compaigns));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Compaigns, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetCompaignsByAdminDto>>(
                await _compaignsService.GetAll(filter)
            ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string headling)
        {
            if (headling == null || headling.Trim() == "") return BadRequest("headling is required");
            return Ok(_mapper.Map<List<GetCompaignsByAdminDto>>(await _compaignsService.GetAll(c => c.Headling.ToLower().Contains(headling.ToLower()))));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Compaigns> compaigns = await _compaignsService.GetAll();
            var data = _mapper.Map<List<GetCompaignsByAdminDto>>(compaigns.OrderBy(c => c.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = compaigns.Count, data });
        }
        [HttpGet("UserPaggination")]
        public async Task<IActionResult> UserPaggination(int skip = 0, int take = 4)
        {
            List<Compaigns> compaigns = await _compaignsService.GetAll(c => !c.IsDeleted && c.ExpirationDate > DateTime.Now);
            var data = _mapper.Map<List<GetCompaignsDto>>(compaigns.OrderByDescending(c => c.ExpirationDate).Skip(skip).Take(take));
            return Ok(data);
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _compaignsService.IsExist(c => c.Id == id))
            {
                return NotFound();
            }
            Compaigns compaigns = await _compaignsService.GetEntity(c => c.Id == id);
            return Ok(_mapper.Map<GetCompaignsByAdminDto>(compaigns));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            List<Compaigns> compaigns = await _compaignsService.GetAll();
            return Ok(_mapper.Map<List<GetCompaignsByAdminDto>>(compaigns.OrderBy(b => b.CreatedAt)));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCompaignsDto updateCompaignsDto)
        {
            if (id == null || updateCompaignsDto.Id != id)
            {
                return BadRequest("Something went wrong");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (!await _compaignsService.IsExist(c => c.Id == id))
            {
                return NotFound("Compaign is not exist");
            }
            Compaigns compaigns = await _compaignsService.GetEntity(c => c.Id == id);
            string oldPublicId = compaigns.PublicId;
            if (updateCompaignsDto.Image != null)
            {
                if (!_fileService.IsImage(updateCompaignsDto.Image))
                {
                    return BadRequest("you must upload only image");
                }
                else if (!_fileService.IsLengthSuit(updateCompaignsDto.Image, 1000))
                {
                    return BadRequest("Image size must be smaller than 1kb");
                }
                var imageResoult = await _photoAccessor.AddPhoto(updateCompaignsDto.Image);
                compaigns.ImageUrl = imageResoult.SecureUrl.ToString();
                compaigns.PublicId = imageResoult.PublicId;

            }
            _mapper.Map(updateCompaignsDto, compaigns);
            var response = await _compaignsService.Update(compaigns);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                if (updateCompaignsDto.Image != null)
                {
                    await _photoAccessor.DeletePhoto(compaigns.PublicId);
                }

                return BadRequest(response);
            }
            if (updateCompaignsDto.Image != null && oldPublicId != "")
            {
                await _photoAccessor.DeletePhoto(oldPublicId);
            }
            return Ok(response);
        }
    }
}

