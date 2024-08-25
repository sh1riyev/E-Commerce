using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.BrandDto;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IPhotoAccessor _photoAccessor;
        public BrandController(IBrandService brandService, IMapper mapper, IFileService fileService, IPhotoAccessor photoAccessor)
        {
            _brandService = brandService;
            _mapper = mapper;
            _fileService = fileService;
            _photoAccessor = photoAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Brand> brands = await _brandService.GetAll(b => !b.IsDeleted, "Products");
            List<GetBrandDto> getBrandDtos = _mapper.Map<List<GetBrandDto>>(brands.OrderBy(b => b.CreatedAt));
            foreach (var getBrandDto in getBrandDtos)
            {
                getBrandDto.Products = getBrandDto.Products.FindAll(p => !p.IsDeleted);
            }
            return Ok(getBrandDtos);
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpGet("GetForProduct")]
        public async Task<IActionResult> GetForProduct()
        {
            List<Brand> brands = await _brandService.GetAll(b => !b.IsDeleted);
            return Ok(brands.Select(b => new { id = b.Id, name = b.Name }));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandDto createBrandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_fileService.IsImage(createBrandDto.Image))
            {
                return BadRequest("you must upload only image");
            }
            else if (!_fileService.IsLengthSuit(createBrandDto.Image, 1000))
            {
                return BadRequest("Image size must be smaller than 1kb");
            }
            var imageResoult = await _photoAccessor.AddPhoto(createBrandDto.Image);
            Brand brand = _mapper.Map<Brand>(createBrandDto);
            brand.ImageUrl = imageResoult.SecureUrl.ToString();
            brand.PublicId = imageResoult.PublicId;
            var response = await _brandService.Create(brand);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                await _photoAccessor.DeletePhoto(imageResoult.PublicId);
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "SupperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var response = await _brandService.Delete(id);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _brandService.IsExist(b => b.Id == id && !b.IsDeleted))
            {
                return NotFound();
            }
            Brand brand = await _brandService.GetEntity(b => b.Id == id, "Products");
            brand.Products = brand.Products.FindAll(p => !p.IsDeleted);
            return Ok(_mapper.Map<GetBrandDto>(brand));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Filter")]
        public async Task<IActionResult> Filter(FilterStatus filterStatus)
        {
            DateTime last = filterStatus.Status == (int)EntityFilter.GetLastDayCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthCreatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekCreatedByAdmin ? DateTime.Now.AddDays(-1) :
                filterStatus.Status == (int)EntityFilter.GetLastDayDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthDeletedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekDeletedByAdmin ? DateTime.Now.AddDays(-7) :
                filterStatus.Status == (int)EntityFilter.GetLastDayUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastMonthUpdatedByAdmin || filterStatus.Status == (int)EntityFilter.GetLastWeekUpdatedByAdmin ? DateTime.Now.AddDays(-30) : DateTime.Now;
            Expression<Func<Brand, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
            return Ok(_mapper.Map<List<GetBrandByAdmin>>(
                await _brandService.GetAll(filter)
            ));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            if (name == null) return BadRequest("name is required");
            return Ok(_mapper.Map<List<GetBrandByAdmin>>(await _brandService.GetAll(b => b.Name.ToLower().Contains(name.ToLower()))));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {

            List<Brand> brands = await _brandService.GetAll();
            var data = _mapper.Map<List<GetBrandByAdmin>>(brands.OrderBy(b => b.CreatedAt).Skip(skip).Take(take));
            return Ok(new { size = brands.Count, data });

        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetByAdmin/{id}")]
        public async Task<IActionResult> GetByAdmin(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else if (!await _brandService.IsExist(b => b.Id == id))
            {
                return NotFound();
            }
            Brand brand = await _brandService.GetEntity(b => b.Id == id, "Products");
            return Ok(_mapper.Map<GetBrandByAdmin>(brand));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("GetAllByAdmin")]
        public async Task<IActionResult> GetAllByAdmin()
        {
            List<Brand> brands = await _brandService.GetAll(null, "Products");
            return Ok(_mapper.Map<List<GetBrandByAdmin>>(brands.OrderBy(b => b.CreatedAt)));
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBrandDto updateBrandDto)
        {
            if (id == null || updateBrandDto.Id != id)
            {
                return BadRequest("Something went wrong");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (!await _brandService.IsExist(b => b.Id == id))
            {
                return NotFound("Brand is not exist");
            }
            Brand brand = await _brandService.GetEntity(c => c.Id == id);
            string oldPublicId = brand.PublicId;
            if (updateBrandDto.Image != null)
            {
                if (!_fileService.IsImage(updateBrandDto.Image))
                {
                    return BadRequest("you must upload only image");
                }
                else if (!_fileService.IsLengthSuit(updateBrandDto.Image, 1000))
                {
                    return BadRequest("Image size must be smaller than 1kb");
                }
                var imageResoult = await _photoAccessor.AddPhoto(updateBrandDto.Image);
                brand.ImageUrl = imageResoult.SecureUrl.ToString();
                brand.PublicId = imageResoult.PublicId;

            }
            _mapper.Map(updateBrandDto, brand);
            var response = await _brandService.Update(brand);
            if (response.StatusCode != (int)StatusCodes.Status200OK)
            {
                if (updateBrandDto.Image != null)
                {
                    await _photoAccessor.DeletePhoto(brand.PublicId);
                }

                return BadRequest(response);
            }
            if (updateBrandDto.Image != null && oldPublicId != "")
            {
                await _photoAccessor.DeletePhoto(oldPublicId);
            }
            return Ok(response);
        }

    }
}

