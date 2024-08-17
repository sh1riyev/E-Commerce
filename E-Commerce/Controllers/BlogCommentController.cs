using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.BlogCommentDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class BlogCommentController : Controller
    {
        private readonly IBlogCommentService _blogCommentService;
        private readonly IMapper _mapper;
        public BlogCommentController(IBlogCommentService blogCommentService,IMapper mapper)
        {
            _blogCommentService = blogCommentService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCommentDto createBlogCommentDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ResponseObj responseObj = await _blogCommentService.Create(_mapper.Map<BlogComment>(createBlogCommentDto));
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null) return BadRequest("something went wrong");
                ResponseObj responseObj = await _blogCommentService.Delete(id);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBlogCommentDto updateBlogCommentDto)
        {
            try
            {
                if (id == null || id != updateBlogCommentDto.Id) return BadRequest("something went wrong");
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (!await _blogCommentService.IsExist(c => c.Id == id)) return NotFound("comment is not exist");
                BlogComment blogComment = await _blogCommentService.GetEntity(c => c.Id == id);
                _mapper.Map(updateBlogCommentDto, blogComment);
                ResponseObj responseObj = await _blogCommentService.Update(blogComment);
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
                return Ok(_mapper.Map<List<GetBlogCommentDto>>(await _blogCommentService.GetAll(c => !c.IsDeleted, "Blog", "AppUser", "Parent")));
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
                BlogComment blogComment = await _blogCommentService.GetEntity(c => c.Id == id && !c.IsDeleted, "Blog", "AppUser", "Parent");
                if (blogComment == null)
                {
                    return NotFound("comment is not exist");
                }
                return Ok(_mapper.Map<GetBlogCommentDto>(blogComment));
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("getByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            try
            {
                if (id == null) return BadRequest("something went wrog");
                BlogComment blogComment = await _blogCommentService.GetEntity(c => c.Id == id, "Blog", "AppUser");
                if (blogComment == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetBlogCommentByAdminDto>(blogComment));
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
                return Ok(_mapper.Map<List<GetBlogCommentByAdminDto>>(await _blogCommentService.GetAll(null, "Blog", "AppUser")));
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            try
            {
                DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                    filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
                Expression<Func<BlogComment, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetBlogCommentByAdminDto>>(
                    await _blogCommentService.GetAll(filter, "Blog", "AppUser")
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string blogTitle)
        {
            try
            {
                if (blogTitle == null || blogTitle.Trim() == "") return BadRequest("something went wrong");
                return Ok(_mapper.Map<List<GetBlogCommentByAdminDto>>(await _blogCommentService.GetAll(c => c.Blog.Title.ToLower().Contains(blogTitle.ToLower()), "Blog", "AppUser")));
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
                List<BlogComment> blogComments = await _blogCommentService.GetAll(null, "Blog", "AppUser");
                var data = _mapper.Map<List<GetBlogCommentByAdminDto>>(blogComments.OrderBy(c => c.CreatedAt).Skip(skip).Take(take));
                return Ok(new {size=blogComments.Count,data});
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

