using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.SliderDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class SliderController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;
        private readonly IPhotoAccessor _photoAccessor;
        public SliderController(IFileService fileService, ISliderService sliderService, IMapper mapper, IPhotoAccessor photoAccessor)
        {
            _sliderService = sliderService;
            _fileService = fileService;
            _mapper = mapper;
            _photoAccessor = photoAccessor;
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderDto createSliderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!_fileService.IsImage(createSliderDto.Image)) return BadRequest("Upload only image");
            else if (!_fileService.IsLengthSuit(createSliderDto.Image, 1000)) return BadRequest("Image size must be smaller than 1kb");
            var imageResoult = await _photoAccessor.AddPhoto(createSliderDto.Image);
            Slider slider = _mapper.Map<Slider>(createSliderDto);
            slider.ImageUrl = imageResoult.SecureUrl.ToString();
            slider.PublicId = imageResoult.PublicId;
            ResponseObj responseObj = await _sliderService.Create(slider);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                await _photoAccessor.DeletePhoto(imageResoult.PublicId);
                return BadRequest(responseObj);
            }
            return Ok(responseObj);
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _sliderService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSliderDto updateSliderDto)
        {
            if (id != updateSliderDto.Id || id == null) return BadRequest();
            else if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _sliderService.IsExist(s => s.Id == id))
            {
                return NotFound("slider is not exist");
            }
            Slider slider = await _sliderService.GetEntity(s => s.Id == id);
            string oldPublicId = slider.PublicId;
            if (updateSliderDto.Image != null)
            {
                if (!_fileService.IsImage(updateSliderDto.Image)) return BadRequest("Upload only image");
                else if (!_fileService.IsLengthSuit(updateSliderDto.Image, 1000)) return BadRequest("Image size must be smaller than 1kb");
                var imageResoult = await _photoAccessor.AddPhoto(updateSliderDto.Image);
                slider.ImageUrl = imageResoult.SecureUrl.ToString();
                slider.PublicId = imageResoult.PublicId;
            }
            _mapper.Map(updateSliderDto, slider);
            ResponseObj responseObj = await _sliderService.Update(slider);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                if (updateSliderDto.Image != null)
                {
                    await _photoAccessor.DeletePhoto(slider.PublicId);
                }
            }
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            if (updateSliderDto.Image != null && oldPublicId != "")
            {
                await _photoAccessor.DeletePhoto(oldPublicId);
            }
            return Ok(responseObj);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<GetSliderDto>>(await _sliderService.GetAll(s => !s.IsDeleted)));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _sliderService.GetEntity(s => s.Id == id && !s.IsDeleted);
            if (slider == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetSliderDto>(slider));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("getByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            Slider slider = await _sliderService.GetEntity(s => s.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetSliderByAdminDto>(slider));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetSliderByAdminDto>>(await _sliderService.GetAll()));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Slider, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetSliderByAdminDto>>(
                await _sliderService.GetAll(filter)
            ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string title)
        {
            if (title == null) return BadRequest("Title is required");
            return Ok(_mapper.Map<List<GetSliderByAdminDto>>(await _sliderService.GetAll(s => s.Title.ToLower().Contains(title.ToLower()))));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Slider> sliders = await _sliderService.GetAll();
            var data = _mapper.Map<List<GetSliderByAdminDto>>(sliders.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = sliders.Count, data });
        }
    }
}

