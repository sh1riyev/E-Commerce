using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.ContactDto;
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
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly ISendEmail _sendEmail;
        public ContactController(IContactService contactService,IMapper mapper,ISendEmail sendEmail)
        {
            _contactService = contactService;
            _mapper = mapper;
            _sendEmail = sendEmail;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateContactDto createContactDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ResponseObj responseObj = await _contactService.Create(_mapper.Map<Contact>(createContactDto));
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPut("ResponseMessage/{id}")]
        public async Task<IActionResult>ResponseMessage(string id, string messageBody,string subject)
        {
            try
            {
                if (id == null||messageBody==null||messageBody.Trim()=="") return BadRequest("something went wrong");
                else if (!await _contactService.IsExist(c => c.Id == id)) return NotFound("contact is not exist");
                Contact contact = await _contactService.GetEntity(c => c.Id == id);
                contact.IsResponded = true;
                contact.RespondedAt = DateTime.Now;
                ResponseObj responseObj = await _contactService.Update(contact);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                _sendEmail.Send("rufatri@code.edu.az", "Allup", contact.Email, messageBody, subject);
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
                ResponseObj responseObj = await _contactService.Delete(id);
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
        public async Task<IActionResult>Update(string id,UpdateContactDto updateContactDto)
        {
            try
            {
                if (id == null || id != updateContactDto.Id) return BadRequest();
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (!await _contactService.IsExist(c => c.Id == id)) return NotFound("contact is not exist");
                Contact contact = await _contactService.GetEntity(c => c.Id == id);
                _mapper.Map(updateContactDto, contact);
                ResponseObj responseObj = await _contactService.Update(contact);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
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
                return Ok(_mapper.Map<List<GetContactDto>>(await _contactService.GetAll(c => !c.IsDeleted)));
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
                return Ok(_mapper.Map<List<GetContactByAdminDto>>(await _contactService.GetAll()));
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                Contact contact = await _contactService.GetEntity(c => c.Id == id && !c.IsDeleted);
                if (contact == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetContactDto>(contact));
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
                if (id == null) return BadRequest();
                Contact contact = await _contactService.GetEntity(c => c.Id == id);
                if (contact == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetContactByAdminDto>(contact));
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
                Expression<Func<Contact, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetContactByAdminDto>>(
                    await _contactService.GetAll(filter)
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
                if (name == null) return BadRequest("something went wrong");
                return Ok(_mapper.Map<List<GetContactByAdminDto>>(await _contactService.GetAll(s => s.Name.ToLower().Contains(name.ToLower()))));
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
                List<Contact> contacts = await _contactService.GetAll();
                var data = _mapper.Map<List<GetContactByAdminDto>>(contacts.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
                return Ok(new {size=contacts.Count,data,pendingCount=(await _contactService.GetAll(c=>!c.IsResponded)).Count});
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

