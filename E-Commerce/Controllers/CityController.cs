using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.CityDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Authorize(Roles = "Admin,SupperAdmin")]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService,IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCityDto createCityDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                City city = _mapper.Map<City>(createCityDto);
                ResponseObj responseObj = await _cityService.Create(city);
                if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("something went wrong");
                }
                ResponseObj responseObj = await _cityService.Delete(id);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<City> cities = await _cityService.GetAll(c => !c.IsDeleted, "Country");
                List<GetCityDto> getCities = _mapper.Map<List<GetCityDto>>(cities);
                return Ok(getCities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetByCountry")]
        public async Task<IActionResult> GetByCountry(string countryId)
        {
            try
            {
                List<City> cities = await _cityService.GetAll(c => !c.IsDeleted&&c.CountryId==countryId, "Country");
                List<GetCityDto> getCities = _mapper.Map<List<GetCityDto>>(cities);
                return Ok(getCities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("something went wrong");
                }
                else if (!await _cityService.IsExist(c => c.Id == id && !c.IsDeleted))
                {
                    return NotFound("City not founded");
                }
                City city = await _cityService.GetEntity(c => c.Id == id, "Country");
                return Ok(_mapper.Map<GetCityDto>(city));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetLastDayCreatedByAdmin")]
        public async Task<IActionResult> GetLastDayCreatedByAdmin()
        {
            try
            {
                DateTime last = DateTime.Now.AddDays(-1); // Get the date of the last day

                // Filter entitys created in the last day
                Expression<Func<City, bool>> filter = entity => entity.CreatedAt >= last;

                return Ok(_mapper.Map<List<GetCityByAdminDto>>(
                    await _cityService.GetAll(filter, "Country")
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetLastWeekCreatedByAdmin")]
        public async Task<IActionResult> GetLastWeekCreatedByAdmin()
        {
            try
            {
                DateTime last = DateTime.Now.AddDays(-7); // Get the date of the last day

                // Filter entitys created in the last day
                Expression<Func<City, bool>> filter = entity => entity.CreatedAt >= last;

                return Ok(_mapper.Map<List<GetCityByAdminDto>>(
                    await _cityService.GetAll(filter, "Country")
                ));
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
                Expression<Func<City, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetCityByAdminDto>>(
                   await _cityService.GetAll(filter, "Country")
               ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            try
            {
                if (name == null) return BadRequest("Name is required");
                return Ok(_mapper.Map<List<GetCityByAdminDto>>(await _cityService.GetAll(c => c.Name.ToLower().Contains(name.ToLower()), "Country")));
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
                List<City> cities = await _cityService.GetAll(null, "Country");
                var data = _mapper.Map<List<GetCityByAdminDto>>(cities.OrderBy(c => c.CreatedAt).Skip(skip).Take(take));
                return Ok(new { size = cities.Count, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                else if (!await _cityService.IsExist(c => c.Id == id))
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetCityByAdminDto>(await _cityService.GetEntity(c => c.Id == id, "Country")));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            try
            {
                return Ok(_mapper.Map<List<GetCityByAdminDto>>(await _cityService.GetAll(null, "Country")));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCityDto updateCityDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (id == null || id != updateCityDto.Id) return BadRequest("something went wrong");
                else if (!await _cityService.IsExist(c => c.Id == id))
                {
                    return NotFound("Country is not exist");
                }
                City city = await _cityService.GetEntity(c => c.Id == id);
                _mapper.Map(updateCityDto, city);
                ResponseObj responseObj = await _cityService.Update(city);
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

