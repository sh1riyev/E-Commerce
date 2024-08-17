using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.DTOs.FilterDto;
using E_Commerce.Business.DTOs.ProductCommentDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.Helpers;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class ProductCommentController : Controller
    {
        private readonly IProductCommentService _productCommentService;
        private readonly IMapper _mapper;
        public ProductCommentController(IProductCommentService productCommentService,IMapper mapper)
        {
            _productCommentService = productCommentService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult>Create(CreateProductCommentDto createProductCommentDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ResponseObj responseObj = await _productCommentService.Create(_mapper.Map<ProductComment>(createProductCommentDto));
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
        public async Task<IActionResult>Delete(string id)
        {
            try
            {
                if (id == null) return BadRequest("something went wrong");
                ResponseObj responseObj = await _productCommentService.Delete(id);
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
        public async Task<IActionResult>Update(string id,UpdateProductCommentDto updateProductCommentDto)
        {
            try
            {
                if (id == null || id != updateProductCommentDto.Id) return BadRequest("something went wrong");
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                else if (!await _productCommentService.IsExist(c => c.Id == id)) return NotFound("entity is not exist");
                ProductComment productComment = await _productCommentService.GetEntity(c => c.Id == id);
                _mapper.Map(updateProductCommentDto, productComment);
                ResponseObj responseObj = await _productCommentService.Update(productComment);
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
                return Ok(_mapper.Map<List<GetProductCommentDto>>(await _productCommentService.GetAll(pc => !pc.IsDeleted, "Product", "AppUser")));
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
                ProductComment productComment = await _productCommentService.GetEntity(c => c.Id == id && !c.IsDeleted, "Product", "AppUser");
                if (productComment == null)
                {
                    return NotFound("entity is not exist");
                }
                return Ok(_mapper.Map<GetProductCommentDto>(productComment));
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
                if (id == null) return BadRequest("something went wrong");
                ProductComment productComment = await _productCommentService.GetEntity(c => c.Id == id, "Product", "AppUser");
                if (productComment == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<GetProductCommentByAdminDto>(productComment));
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
                return Ok(_mapper.Map<List<GetProductCommentByAdminDto>>(await _productCommentService.GetAll(null, "Product", "AppUser")));
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
                Expression<Func<ProductComment, bool>> filter = entity => filterStatus.Status > 0 && filterStatus.Status < 4 ? entity.CreatedAt >= last : filterStatus.Status > 3 && filterStatus.Status < 7 ? entity.DeletedAt >= last : filterStatus.Status > 6 && filterStatus.Status < 10 ? entity.UpdatedAt >= last : default;
                return Ok(_mapper.Map<List<GetProductCommentByAdminDto>>(
                    await _productCommentService.GetAll(filter, "Product", "AppUser")
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string productName)
        {
            try
            {
                if (productName == null || productName.Trim() == "") return BadRequest("something went wrong");
                return Ok(_mapper.Map<List<GetProductCommentByAdminDto>>(await _productCommentService.GetAll(c => c.Product.Name.ToLower().Contains(productName.ToLower()), "Product", "AppUser")));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin")]
        [HttpGet("Paggination")]
        public async Task<IActionResult> Paginnation(int skip = 0, int take = 4)
        {
            try
            {
                List<ProductComment> productComments = await _productCommentService.GetAll(null, "Product", "AppUser");
                var data = _mapper.Map<List<GetProductCommentByAdminDto>>(productComments.OrderBy(c => c.CreatedAt).Skip(skip).Take(take));
                return Ok(new {size=productComments.Count,data});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("ClientFeedback")]
        public async Task<IActionResult> ClientFeedback(int skip = 0, int take = 4)
        {
            try
            {
                List<ProductComment> productComments = await _productCommentService.GetAll(c=>!c.IsDeleted&&c.Content.Length>30&&c.Rating==5, "Product", "AppUser");
                var data = _mapper.Map<List<GetProductCommentDto>>(productComments.OrderByDescending(c => c.CreatedAt).Skip(skip).Take(take));
                return Ok( data );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}


