using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using E_Commerce.Business.DTOs.AccountDto;
using E_Commerce.Business.DTOs.CheckDto;
using E_Commerce.Business.DTOs.ProductDto;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Business.DTOs.UserDto;
using E_Commerce.Business.Interfaces;
using E_Commerce.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        // GET: api/values
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ICheckService _checkService;
        private readonly IDistributedCache _distributedCache;
        public AccountController(IMapper mapper,IAccountService accountService,IUserService userService,ICheckService checkService,IDistributedCache distributedCache)
        {
            _mapper = mapper;
            _accountService = accountService;
            _userService = userService;
            _checkService = checkService;
            _distributedCache = distributedCache;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)return BadRequest(ModelState);
                else if (await _accountService.IsExist(u => u.UserName.ToLower() == userRegisterDto.UserName.ToLower()))
                {
                    return BadRequest("UserName must be unique for every users");
                }
                AppUser appUser = _mapper.Map<AppUser>(userRegisterDto);
                appUser.CreatedAt = DateTime.Now;
                appUser.IsActive=userRegisterDto.IsSeller == true ? false : true;
                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host.Value;

                ResponseObj responseObj = await _accountService.Register(appUser, userRegisterDto.Password);
                if (responseObj.StatusCode==(int)StatusCodes.Status400BadRequest)
                {
                    return BadRequest(responseObj.ResponseMessage);
                }
                
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPost("VerifyEmailWithOTP")]
        public async Task<IActionResult> VerifyEmailWithOTP(string verifyEmail,string otp)
        {
            try
            {
                if (!int.TryParse(otp, out int intOTP)||VerifyEmail == null || otp == null||otp.Length!=6) return BadRequest("invalid email or OTP");
                ResponseObj responseObj = await _accountService.VerifyEmailWithOTP(verifyEmail, otp);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
                return Ok(responseObj.ResponseMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string verifyEmail, string token)
        {
            try
            {
                if (VerifyEmail == null || token == null) return BadRequest("invalid email or token");
                ResponseObj responseObj = await _accountService.VerifyEmail(verifyEmail, token);
                if (responseObj.StatusCode==(int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
                return Ok(responseObj.ResponseMessage);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ResponseObj responseObj = await _accountService.Login(loginDto);
            if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            return Ok(responseObj.ResponseMessage);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                if (email == null) return BadRequest("invalid email");
                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host.Value;
                if (email == null) return BadRequest("email is not valid");
                ResponseObj responseObj = await _accountService.ForgetPassword(email, scheme, host);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
                return Ok(responseObj.ResponseMessage);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
      
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)  return BadRequest(ModelState);
                ResponseObj responseObj = await _accountService.ResetPassword(userResetPasswordDto);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
                return Ok(responseObj.ResponseMessage);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult>ProfileUpdate(string id,[FromForm]UserUpdateDto userUpdateDto)
        {
            try
            {
                if (id == null) return BadRequest("something went wrong");
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host.Value;
                ResponseObj responseObj = await _accountService.ProfileUpdate(id,userUpdateDto,scheme,host);
                if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
                else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);
                return Ok(responseObj.ResponseMessage);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("IsExist/{id}")]
        public async Task<IActionResult> IsExist(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return Ok(false);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id&&!u.IsDeleted&&u.IsActive)) return NotFound("user is not exist");
               GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Blogs", "Wishlists");
                return Ok(_mapper.Map<GetUserDetailDto>(getUserDto));
            }
            catch (Exception ex)
            {
                 return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("UserWishlist/{id}")]
        public async Task<IActionResult> GetUserWishlist(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Wishlists.Product.ProductImages", "Wishlists.Product.Category", "Wishlists.Product.Brand");
                
                getUserDto.Products = null;
                getUserDto.Wishlists = getUserDto.Wishlists.FindAll(w => !w.IsDeleted);

                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("GetUserChecks/{id}")]
        public async Task<IActionResult> GetUserChecks(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Checkes.CheckProducts.Product.ProductImages", "Checkes.CheckProducts.Product.Seller", "Checkes.Adress");

                getUserDto.Products = null;
                getUserDto.Adresses = null;
                getUserDto.Checkes = getUserDto.Checkes.Where(w => !w.IsDeleted).OrderByDescending(c=>c.CreatedAt).ToList();

                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin,SupperAdmin,Seller")]
        [HttpGet("GetUserOrders/{id}")]
        public async Task<IActionResult> GetUserOrders(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id);
                List<Check> checks = await _checkService.GetAll(c => !c.IsDeleted, "Adress.City.Country", "CheckProducts.Product.ProductImages", "AppUser");
                
                foreach (var check in checks)
                {
                    check.CheckProducts = check.CheckProducts.Where(cp => cp.Product.SellerId == id).ToList();
                }
                getUserDto.Products = null;
                getUserDto.Adresses = null;
                getUserDto.Checkes = _mapper.Map<List<GetCheckDto>>(checks);
                getUserDto.Checkes = getUserDto.Checkes.Where(w => !w.IsDeleted&&w.CheckProducts.Count>0).OrderByDescending(c => c.CreatedAt).ToList();
                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("GetUserMessages/{id}")]
        public async Task<IActionResult> GetUserMessages(string id,string toId)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                else if (!await _accountService.IsExist(u => u.Id == toId && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "SentMessages", "ReceivedMessages");
                getUserDto.SentMessages = getUserDto.SentMessages.Where(m => m.ToUserId == toId).ToList();
                getUserDto.ReceivedMessages = getUserDto.ReceivedMessages.Where(m => m.FromUserId == toId).ToList();
                getUserDto.Products = null;
                getUserDto.Adresses = null;

                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("UserBasket/{id}")]
        public async Task<IActionResult> GetUserBasket(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Baskets.Product.ProductImages", "Baskets.Product.Category");
                getUserDto.Products = null;
                getUserDto.Wishlists = null;
                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("UserProduct/{id}")]
        public async Task<IActionResult> GetUserProduct(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Products.ProductImages", "Products.Category", "Products.Brand", "Products.ProductTags.Tag");
                getUserDto.Wishlists = null;
                getUserDto.Products = getUserDto.Products.OrderBy(p=>p.CreatedAt).ToList().FindAll(p => !p.IsDeleted&&p.IsAccepted);

                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("UserAdress/{id}")]
        public async Task<IActionResult> GetUserAdress(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Adresses.City.Country");
                getUserDto.Adresses = getUserDto.Adresses.OrderBy(p => p.CreatedAt).ToList().FindAll(p => !p.IsDeleted);

                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("ProductSearch/{id}")]
        public async Task<IActionResult> ProductSearch(string id,string name)
        {
            try
            {
                if (name == null || name.Trim() == ""|| id == null) return BadRequest("something went wrong");
                else if (!await _accountService.IsExist(u => u.Id == id & !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Products.ProductImages", "Products.Category", "Products.Brand", "Products.ProductTags.Tag", "Products.ProductComments");
                if (getUserDto == null || getUserDto.IsDeleted || !getUserDto.IsActive) return NotFound("user is not exist");
                getUserDto.Wishlists = null;
                int size = getUserDto.Products.Where(p => !p.IsDeleted&&p.IsAccepted).Count();
                getUserDto.Products = getUserDto.Products.OrderByDescending(p => p.CreatedAt).Where(p=>!p.IsDeleted&&p.IsAccepted&&p.Name.ToLower().Contains(name)).ToList();
                foreach (var product in getUserDto.Products)
                {
                    product.ProductComments = product.ProductComments.FindAll(p => !p.IsDeleted);
                }
                return Ok(getUserDto);
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("ProductPaginnation/{id}")]
        public async Task<IActionResult> ProductPaginnation(string id,int skip = 0, int take = 4)
        {
            try
            {

                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Products.ProductImages", "Products.Category", "Products.Brand", "Products.ProductTags.Tag", "Products.ProductComments");
                if (getUserDto == null || getUserDto.IsDeleted || !getUserDto.IsActive) return NotFound("user is not exist");
                getUserDto.Wishlists = null;
                int size = getUserDto.Products.Where(p => !p.IsDeleted&&p.IsAccepted).Count();
                getUserDto.Products = getUserDto.Products.OrderByDescending(p => p.CreatedAt).Where(p => !p.IsDeleted&&p.IsAccepted).Skip(skip).Take(take).ToList();
                foreach (var product in getUserDto.Products)
                {
                    product.ProductComments = product.ProductComments.FindAll(p => !p.IsDeleted);
                }
                return Ok(new {data= getUserDto ,size});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("UserBlog/{id}")]
        public async Task<IActionResult> GetUserBlog(string id)
        {
            try
            {
                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Blogs.BlogTags.Tag", "Blogs.BlogComments");
                if (getUserDto == null || getUserDto.IsDeleted || !getUserDto.IsActive) return NotFound("user is not exist");
                getUserDto.Blogs = getUserDto.Blogs.FindAll(b => !b.IsDeleted);

                foreach (var blog in getUserDto.Blogs)
                {
                    blog.BlogComments = blog.BlogComments.FindAll(p => !p.IsDeleted);
                }
                return Ok(getUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("BlogSearch/{id}")]
        public async Task<IActionResult> BlogSearch(string id, string title)
        {
            try
            {
                if (title == null || title.Trim() == "" || id == null) return BadRequest("something went wrong");
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Blogs.BlogTags.Tag", "Blogs.BlogComments");
                if (getUserDto == null || getUserDto.IsDeleted || !getUserDto.IsActive) return NotFound("user is not exist");
                
                int size = getUserDto.Blogs.Where(p => !p.IsDeleted).Count();
                getUserDto.Blogs = getUserDto.Blogs.OrderByDescending(p => p.CreatedAt).Where(p => !p.IsDeleted && p.Title.ToLower().Contains(title)).ToList();
                foreach (var blog in getUserDto.Blogs)
                {
                    blog.BlogComments = blog.BlogComments.FindAll(p => !p.IsDeleted);
                }
                return Ok(getUserDto);
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("BlogPaginnation/{id}")]
        public async Task<IActionResult> BlogPaginnation(string id, int skip = 0, int take = 4)
        {
            try
            {

                if (id == null) return BadRequest();
                else if (!await _accountService.IsExist(u => u.Id == id && !u.IsDeleted && u.IsActive)) return NotFound("user is not exist");
                GetUserDto getUserDto = await _userService.GetUser(u => u.Id == id, "Blogs.BlogTags.Tag", "Blogs.BlogComments");
                if (getUserDto == null || getUserDto.IsDeleted || !getUserDto.IsActive) return NotFound("user is not exist");
                int size = getUserDto.Blogs.Where(p => !p.IsDeleted).Count();
                getUserDto.Blogs = getUserDto.Blogs.OrderByDescending(p => p.CreatedAt).Where(p => !p.IsDeleted).Skip(skip).Take(take).ToList();
                foreach (var blog in getUserDto.Blogs)
                {
                    blog.BlogComments = blog.BlogComments.FindAll(p => !p.IsDeleted);
                }
                return Ok(new { data = getUserDto, size });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}

