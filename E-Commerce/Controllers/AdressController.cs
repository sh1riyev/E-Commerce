using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.AdressDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce .Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using   E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdressController : Controller
    {
        private readonly IAdressService _adressService;
        private readonly IMapper _mapper;
        public AdressController(IAdressService adressService,IMapper mapper)
        {
            _adressService = adressService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdressDto createAdressDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ResponseObj responseObj = await _adressService.Create(_mapper.Map<Address>(createAdressDto));
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                ResponseObj responseObj = await _adressService.Delete(id);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateAdressDto updateAdressDto)
        {
            try
            {
                if (id == null || id != updateAdressDto.Id) return BadRequest();
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (!await _adressService.IsExist(a => a.Id == id)) return NotFound("adress is not exist");
                Address adress = await _adressService.GetEntity(a => a.Id == id);
                _mapper.Map(updateAdressDto, adress);
                ResponseObj responseObj = await _adressService.Update(adress);
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
                return Ok(_mapper.Map<List<GetAdressDto>>(await _adressService.GetAll(a => !a.IsDeleted, "AppUser", "City.Country")));
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
                return Ok(_mapper.Map<List<GetAdressByAdmin>>(await _adressService.GetAll(null, "AppUser", "City.Country")));
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
                if (id == null) return BadRequest("something went wrong");
                Address adress = await _adressService.GetEntity(a => a.Id == id && !a.IsDeleted, "AppUser", "City.Country");
                if (adress == null)
                {
                    return NotFound("adress is not exist");
                }
                return Ok(_mapper.Map<GetAdressDto>(adress));
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
                Address adress = await _adressService.GetEntity(c => c.Id == id, "AppUser", "City.Country");
                if (adress == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetAdressByAdmin>(adress));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult>Filter(FilterStatus filterStatus)
        {
            try
            {
                DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
                Expression<Func<Address, bool>> filter = entity =>filterStatus.Status>0&&filterStatus.Status<4? entity.CreatedAt >= last: filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last: filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last:default;
                return Ok(_mapper.Map<List<GetAdressByAdmin>>(
                            await _adressService.GetAll(filter, "AppUser", "City.Country")
                        ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string street)
        {
            try
            {
                if (street == null||street.Trim()=="") return BadRequest("something went wrong");
                return Ok(_mapper.Map<List<GetAdressByAdmin>>(await _adressService.GetAll(s => s.Street.ToLower().Contains(street.ToLower()), "AppUser")));
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
                List<Address> adresses = await _adressService.GetAll(null, "AppUser", "City.Country");
                var data = _mapper.Map<List<GetAdressByAdmin>>(adresses.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
                return Ok(new { size = adresses.Count, data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

