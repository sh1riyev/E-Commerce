﻿using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.AdressDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdressController : Controller
    {
        private readonly IAdressService _adressService;
        private readonly IMapper _mapper;
        public AdressController(IAdressService adressService, IMapper mapper)
        {
            _adressService = adressService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdressDto createAdressDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ResponseObj responseObj = await _adressService.Create(_mapper.Map<Adress>(createAdressDto));
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _adressService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateAdressDto updateAdressDto)
        {
            if (id == null || id != updateAdressDto.Id) return BadRequest();
            else if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _adressService.IsExist(a => a.Id == id)) return NotFound("adress is not exist");
            Adress adress = await _adressService.GetEntity(a => a.Id == id);
            _mapper.Map(updateAdressDto, adress);
            ResponseObj responseObj = await _adressService.Update(adress);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<GetAdressDto>>(await _adressService.GetAll(a => !a.IsDeleted, "AppUser", "City.Country")));

        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {

            return Ok(_mapper.Map<List<GetAdressByAdmin>>(await _adressService.GetAll(null, "AppUser", "City.Country")));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest("something went wrong");
            Adress adress = await _adressService.GetEntity(a => a.Id == id && !a.IsDeleted, "AppUser", "City.Country");
            if (adress == null)
            {
                return NotFound("adress is not exist");
            }
            return Ok(_mapper.Map<GetAdressDto>(adress));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            Adress adress = await _adressService.GetEntity(c => c.Id == id, "AppUser", "City.Country");
            if (adress == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetAdressByAdmin>(adress));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Adress, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetAdressByAdmin>>(
                        await _adressService.GetAll(filter, "AppUser", "City.Country")
                    ));
        }

        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string street)
        {

            if (street == null || street.Trim() == "") return BadRequest("something went wrong");
            return Ok(_mapper.Map<List<GetAdressByAdmin>>(await _adressService.GetAll(s => s.Street.ToLower().Contains(street.ToLower()), "AppUser")));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Adress> adresses = await _adressService.GetAll(null, "AppUser", "City.Country");
            var data = _mapper.Map<List<GetAdressByAdmin>>(adresses.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = adresses.Count, data });
        }
    }
}

