using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.BlogDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IPhotoAccessor _photoAccessor;
        public BlogController(IBlogService blogService, IFileService fileService, IMapper mapper, IPhotoAccessor photoAccessor)
        {
            _blogService = blogService;
            _fileService = fileService;
            _mapper = mapper;
            _photoAccessor = photoAccessor;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDto createBlogDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!_fileService.IsImage(createBlogDto.Image)) return BadRequest("upload only image");
            else if (!_fileService.IsLengthSuit(createBlogDto.Image, 1000)) return BadRequest(" image length must be smaller than 1kb");
            Blog blog = _mapper.Map<Blog>(createBlogDto);
            blog.Id = Guid.NewGuid().ToString();
            var resoult = await _photoAccessor.AddPhoto(createBlogDto.Image);
            blog.ImageUrl = resoult.SecureUrl.ToString();
            blog.PublicId = resoult.PublicId;
            foreach (var tag in createBlogDto.TagIds)
            {
                blog.BlogTags.Add(new BlogTags
                {
                    TagId = tag,
                    BlogId = blog.Id,

                });
            }
            ResponseObj responseObj = await _blogService.Create(blog);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                await _photoAccessor.DeletePhoto(blog.PublicId);
            }
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest("something went wrong");
            ResponseObj responseObj = await _blogService.Delete(id);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            return Ok(responseObj);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBlogDto updateBlogDto)
        {
            if (id == null || id != updateBlogDto.Id) return BadRequest("something went wrong");
            else if (!ModelState.IsValid) return BadRequest(ModelState);
            else if (!await _blogService.IsExist(b => b.Id == id)) return NotFound("not found");
            Blog blog = await _blogService.GetEntity(b => b.Id == id);
            string oldImagePublicId = blog.PublicId;

            if (updateBlogDto.TagIds != null)
            {
                blog = await _blogService.GetEntity(b => b.Id == id, "BlogTags.Tag");
            }
            if (updateBlogDto.TagIds != null)
            {
                foreach (var tag in updateBlogDto.TagIds)
                {
                    blog.BlogTags.Add(new BlogTags
                    {
                        Id = Guid.NewGuid().ToString(),
                        TagId = tag,
                        BlogId = blog.Id,

                    });
                }
            }
            if (updateBlogDto.Image != null)
            {
                if (!_fileService.IsImage(updateBlogDto.Image)) return BadRequest("upload only image");
                else if (!_fileService.IsLengthSuit(updateBlogDto.Image, 1000)) return BadRequest(" image length must be smaller than 1kb");
                var resoult = await _photoAccessor.AddPhoto(updateBlogDto.Image);
                blog.ImageUrl = resoult.SecureUrl.ToString();
                blog.PublicId = resoult.PublicId;
            }
            _mapper.Map(updateBlogDto, blog);
            ResponseObj responseObj = await _blogService.Update(blog);
            if (responseObj.StatusCode != (int)StatusCodes.Status200OK)
            {
                if (updateBlogDto.Image != null)
                {
                    await _photoAccessor.DeletePhoto(blog.PublicId);
                }
            }
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            if (updateBlogDto.Image != null && oldImagePublicId != null)
            {
                await _photoAccessor.DeletePhoto(oldImagePublicId);
            }
            return Ok(responseObj);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Blog> blogs = await _blogService.GetAll(b => !b.IsDeleted, "AppUser", "BlogTags.Tag", "BlogComments.AppUser", "BlogComments.Parent");
            foreach (var blog in blogs)
            {
                blog.BlogComments = blog.BlogComments.OrderBy(c => c.CreatedAt).ToList().FindAll(c => !c.IsDeleted);
                for (int i = 0; i < blog.BlogComments.Count; i++)
                {
                    if (blog.BlogComments[i].Parent != null)
                    {
                        blog.BlogComments[i].Parent = blog.BlogComments[i].Parent.IsDeleted ? null : blog.BlogComments[i].Parent;

                    }
                }

            }
            return Ok(_mapper.Map<List<GetBlogDto>>(blogs));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {

            return Ok(_mapper.Map<List<GetBlogByAdminDto>>(await _blogService.GetAll(null, "AppUser", "BlogTags.Tag", "BlogComments.AppUser", "BlogComments.Parent")));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return BadRequest();
            Blog blog = await _blogService.GetEntity(b => b.Id == id && !b.IsDeleted, "AppUser", "BlogComments.AppUser", "BlogComments.Parent");
            if (blog == null)
            {
                return NotFound();
            }
            blog.ViewCount += 1;
            var responseObj = await _blogService.Update(blog);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj);
            blog.BlogComments = blog.BlogComments.OrderBy(c => c.CreatedAt).ToList().FindAll(c => !c.IsDeleted);
            for (int i = 0; i < blog.BlogComments.Count; i++)
            {
                if (blog.BlogComments[i].Parent != null)
                {
                    blog.BlogComments[i].Parent = blog.BlogComments[i].Parent.IsDeleted ? null : blog.BlogComments[i].Parent;

                }
            }
            return Ok(_mapper.Map<GetBlogDto>(await _blogService.GetEntity(b => b.Id == id && !b.IsDeleted, "AppUser", "BlogTags.Tag", "BlogComments.AppUser", "BlogComments.Parent")));
        }
        [Authorize]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null) return BadRequest();
            Blog blog = await _blogService.GetEntity(b => b.Id == id, "AppUser", "BlogTags.Tag", "BlogComments.AppUser", "BlogComments.Parent");
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetBlogByAdminDto>(blog));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Blog, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetBlogByAdminDto>>(
               await _blogService.GetAll(filter, "AppUser", "BlogTags.Tag")
           ));
        }

        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string title)
        {
            if (title == null || title.Trim() == "") return BadRequest("something went wrong");
            return Ok(_mapper.Map<List<GetBlogByAdminDto>>(await _blogService.GetAll(p => p.Title.ToLower().Contains(title.ToLower()), "AppUser", "BlogTags.Tag")));

        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            List<Blog> blogs = await _blogService.GetAll(null, "AppUser", "BlogTags.Tag");
            var data = _mapper.Map<List<GetBlogByAdminDto>>(blogs.OrderBy(p => p.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = blogs.Count, data });
        }
        [HttpGet("UserSearch")]
        public async Task<IActionResult> UserSearch(string title)
        {
            if (title == null || title.Trim() == "") return BadRequest("something went wrong");
            List<Blog> blogs = await _blogService.GetAll(p => !p.IsDeleted && p.Title.ToLower().Contains(title.ToLower()), "AppUser", "BlogTags.Tag");
            foreach (var blog in blogs)
            {
                blog.BlogComments = blog.BlogComments.FindAll(c => !c.IsDeleted);
            }
            return Ok(_mapper.Map<List<GetBlogDto>>(blogs));
        }
        [HttpGet("UserPaggination")]
        public async Task<IActionResult> UserPaginnation(int skip = 0, int take = 4)
        {
            List<Blog> blogs = await _blogService.GetAll(b => !b.IsDeleted, "AppUser", "BlogTags.Tag", "BlogComments");
            foreach (var blog in blogs)
            {
                blog.BlogComments = blog.BlogComments.FindAll(c => !c.IsDeleted);
            }
            var data = _mapper.Map<List<GetBlogDto>>(blogs.OrderByDescending(p => p.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = blogs.Count, data });
        }
    }
}

