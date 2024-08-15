using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.CategoryDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;
using E_Commerce.DTOs.CategoryDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        // GET: api/values
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPhotoAccessor _photoAccessor;
        public CategoryController(ICategoryService categoryService,IMapper mapper,IFileService fileService,IWebHostEnvironment webHostEnvironment,IPhotoAccessor photoAccessor)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _photoAccessor = photoAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                List<Category> categoties =await _categoryService.GetAll(c=>!c.IsDeleted, "SubCategories", "Products", "Parent");
                List<GetCategoryDto> getCategories = _mapper.Map<List<GetCategoryDto>>(categoties.OrderBy(c=>c.CreatedAt));
                foreach (var getCategory in getCategories)
                {
                    getCategory.Products = getCategory.Products.FindAll(p => !p.IsDeleted);
                    getCategory.SubCategories = getCategory.SubCategories.FindAll(c => !c.IsDeleted);
                }
                
                return Ok(getCategories);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpGet("GetForProduct")]
        public async Task<IActionResult> GetForProduct()
        {
            try
            {
                List<Category> categories = await _categoryService.GetAll(c => !c.IsDeleted);
                return Ok(categories.Select(c => new { id = c.Id, name = c.Name }));
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto createCategoryDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!_fileService.IsImage(createCategoryDto.Image))
                {
                    return BadRequest("you must upload only image");
                }
                else if (!_fileService.IsLengthSuit(createCategoryDto.Image,1000))
                {
                    return BadRequest("Image size must be smaller than 1kb");
                }
                if (createCategoryDto.IsMain)
                {
                    createCategoryDto.ParentId = null;
                }
                if (createCategoryDto.ParentId==null)
                {
                    createCategoryDto.IsMain = true;
                }
                var imageResoult = await _photoAccessor.AddPhoto (createCategoryDto.Image);
                Category category =_mapper.Map<Category>(createCategoryDto);
                category.ImageUrl = imageResoult.SecureUrl.ToString();
                category.PublicId = imageResoult.PublicId;
                var response = await _categoryService.Create(category);
                if (response.StatusCode!=(int)StatusCodes.Status200OK)
                {
                    await _photoAccessor.DeletePhoto(imageResoult.PublicId);
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>Get(string id)
        {
            try
            {
                if (id==null)
                {
                    return BadRequest();
                }
                else if (! await _categoryService.IsExist(c=>c.Id==id&&!c.IsDeleted))
                {
                    return NotFound();
                }
                Category category= await _categoryService.GetEntity(c => c.Id == id, "SubCategories", "Products", "Parent");
                category.Products = category.Products.FindAll(p => !p.IsDeleted);
                category.SubCategories = category.SubCategories.FindAll(c => !c.IsDeleted);
                return Ok(_mapper.Map<GetCategoryDto>(category));
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
                Expression<Func<Category, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetCategoryByAdmin>>(
                     await _categoryService.GetAll(filter)
                 ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            try
            {
                if (name == null || name.Trim() == "") return BadRequest("something went wrong");
                return Ok(_mapper.Map<List<GetCategoryByAdmin>>(await _categoryService.GetAll(b =>  b.Name.ToLower().Contains(name.ToLower()))));
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
                List<Category> categories = await _categoryService.GetAll(null, "Parent");
                var data = _mapper.Map<List<GetCategoryByAdmin>>(categories.OrderBy(b => b.CreatedAt).Skip(skip).Take(take));
                return Ok(new {size=categories.Count,data});
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("UserPaggination")]
        public async Task<IActionResult> UserPaggination(int skip = 0, int take = 4)
        {
            try
            {
                List<Category> categories = await _categoryService.GetAll(c=>!c.IsDeleted&&!c.IsMain, "Parent");
                var data = _mapper.Map<List<GetCategoryByAdmin>>(categories.OrderByDescending(b => b.CreatedAt).Skip(skip).Take(take));
                return Ok(data );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetMainCategories")]
        public async Task<IActionResult> GetMainCategories()
        {
            try
            {
                List<Category> categoties = await _categoryService.GetAll(c=>c.IsMain&&!c.IsDeleted);
                var data = categoties.Select(c => new { id = c.Id, name = c.Name });
                return Ok(data);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult>GetByAdmin(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                else if (!await _categoryService.IsExist(c => c.Id == id))
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetCategoryByAdmin>(await _categoryService.GetEntity(c => c.Id == id, "SubCategories", "Products", "Parent")));
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
                if (id==null)
                {
                    return BadRequest("Id is required");
                }
                var response = await _categoryService.Delete(id);
                if (response.StatusCode!=(int)StatusCodes.Status200OK)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult>Update(string id, UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (id==null||id!=updateCategoryDto.Id)
                {
                    return BadRequest();
                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                else if (!await _categoryService.IsExist(c=>c.Id==id))
                {
                    return NotFound();
                }
                if (updateCategoryDto.IsMain)
                {
                    updateCategoryDto.ParentId = null;
                }
                if (updateCategoryDto.ParentId == null)
                {
                    updateCategoryDto.IsMain = true;
                }
                Category category = await _categoryService.GetEntity(c => c.Id == id);
                string oldPublicId = category.PublicId;
                if (updateCategoryDto.Image != null)
                {
                    if (!_fileService.IsImage(updateCategoryDto.Image))
                    {
                        return BadRequest("you must upload only image");
                    }
                    else if (!_fileService.IsLengthSuit(updateCategoryDto.Image, 1000))
                    {
                        return BadRequest("Image size must be smaller than 1kb");
                    }
                     var imageResoult =await _photoAccessor.AddPhoto(updateCategoryDto.Image);
                    category.ImageUrl = imageResoult.SecureUrl.ToString();
                    category.PublicId = imageResoult.PublicId;

                }
                _mapper.Map(updateCategoryDto, category);
                var response = await _categoryService.Update(category);
                if (response.StatusCode!=(int)StatusCodes.Status200OK)
                {
                    if (updateCategoryDto.Image!=null)
                    {
                        await _photoAccessor.DeletePhoto(category.PublicId);
                    }
                   
                    return BadRequest(response);
                }
                if (updateCategoryDto.Image!=null&& oldPublicId!="")
                {
                    await _photoAccessor.DeletePhoto(oldPublicId);
                }
                return Ok(response);
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
                List<Category> categoties = await _categoryService.GetAll(null,"SubCategories", "Products", "Parent");
                List<GetCategoryByAdmin> getCategories = _mapper.Map<List<GetCategoryByAdmin>>(categoties.OrderBy(c=>c.CreatedAt));
                return Ok(getCategories);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }
}

