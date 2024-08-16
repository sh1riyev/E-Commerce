using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.CountryDto;
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
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService,IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCountryDto createCountryDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                Country country = _mapper.Map<Country>(createCountryDto);
                ResponseObj responseObj = await _countryService.Create(country);
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
                ResponseObj responseObj = await _countryService.Delete(id);
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
                List<Country> countries = await _countryService.GetAll(c => !c.IsDeleted);
                List<GetCountryDto> getCountryDtos = _mapper.Map<List<GetCountryDto>>(countries);
                return Ok(getCountryDtos);
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
                else if (!await _countryService.IsExist(c => c.Id == id && !c.IsDeleted))
                {
                    return NotFound("Country not founded");
                }
                Country country = await _countryService.GetEntity(c => c.Id == id);
                return Ok(_mapper.Map<GetCountryDto>(country));
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
                Expression<Func<Country, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetCountryByAdminDto>>(
                    await _countryService.GetAll(filter)
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
                return Ok(_mapper.Map<List<GetCountryByAdminDto>>(await _countryService.GetAll(c => c.Name.ToLower().Contains(name.ToLower()))));
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
                List<Country> countries = await _countryService.GetAll();
                var data = _mapper.Map<List<GetCountryByAdminDto>>(countries.OrderBy(c => c.CreatedAt).Skip(skip).Take(take));
                return Ok(new { size = countries.Count, data });
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
                else if (!await _countryService.IsExist(c => c.Id == id))
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetCountryByAdminDto>(await _countryService.GetEntity(c => c.Id == id)));
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
                return Ok(_mapper.Map<List<GetCountryByAdminDto>>(await _countryService.GetAll()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCountryDto updateCountryDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (id == null || id != updateCountryDto.Id) return BadRequest("something went wrong");
                else if (!await _countryService.IsExist(c => c.Id == id))
                {
                    return NotFound("Country is not exist");
                }
                Country country = await _countryService.GetEntity(c => c.Id == id);
                _mapper.Map(updateCountryDto, country);
                ResponseObj responseObj = await _countryService.Update(country);
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

