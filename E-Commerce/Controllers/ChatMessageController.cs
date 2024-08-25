using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.ChatMessageDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.Business.DTOs.FilterDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Authorize(Roles = "Admin,SupperAdmin")]
    [Route("api/[controller]")]
    public class ChatMessageController : Controller
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public ChatMessageController(IChatMessageService chatMessageService, UserManager<AppUser> userManager, IFileService fileService, IMapper mapper)
        {
            _chatMessageService = chatMessageService;
            _userManager = userManager;
            _fileService = fileService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto sendMessageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ChatMessage chatMessage = _mapper.Map<ChatMessage>(sendMessageDto);
            ResponseObj responseObj = await _chatMessageService.Create(chatMessage);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return await Get(responseObj.ResponseMessage);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest();
            ResponseObj responseObj = await _chatMessageService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<GetMessageDto>>(await _chatMessageService.GetAll(a => !a.IsDeleted, "FromUser", "ToUser")));
        }
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            return Ok(_mapper.Map<List<GetMessageByAdmin>>(await _chatMessageService.GetAll(null, "FromUser", "ToUser")));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest("something went wrong");
            ChatMessage chatMessage = await _chatMessageService.GetEntity(a => a.Id == id && !a.IsDeleted, "FromUser", "ToUser");
            if (chatMessage == null)
            {
                return NotFound("message is not exist");
            }
            return Ok(_mapper.Map<GetMessageDto>(chatMessage));
        }
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            ChatMessage chatMessage = await _chatMessageService.GetEntity(c => c.Id == id, "FromUser", "ToUser");
            if (chatMessage == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetMessageByAdmin>(chatMessage));
        }
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<ChatMessage, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetMessageByAdmin>>(
                await _chatMessageService.GetAll(filter, "FromUser", "ToUser")
            ));
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string fromUserName)
        {
            if (fromUserName == null || fromUserName.Trim() == "") return BadRequest("something went wrong");
            return Ok(_mapper.Map<List<GetMessageByAdmin>>(await _chatMessageService.GetAll(s => s.FromUser.UserName.ToLower().Contains(fromUserName.ToLower()), "FromUser", "ToUser")));
        }
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<ChatMessage> chatMessages = await _chatMessageService.GetAll(null, "FromUser", "ToUser");
            var data = _mapper.Map<List<GetMessageByAdmin>>(chatMessages.OrderBy(s => s.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = chatMessages.Count, data });
        }
    }
}

