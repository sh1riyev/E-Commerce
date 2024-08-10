using System;
using E_Commerce.Business.DTOs.AdressDto;
using E_Commerce.Business.DTOs.BasketDto;
using E_Commerce.Business.DTOs.ChatMessageDto;
using E_Commerce.Business.DTOs.CheckDto;
using E_Commerce.Business.DTOs.WishlistDto;

namespace E_Commerce.Business.DTOs.UserDto
{
	public class GetUserDto
	{
        public string ProfileImageUrl { get; set; }
        public string PublicId { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> RemovedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsSeller { get; set; }
        public List<GetUserProduct> Products { get; set; }
        public List<GetUserBlog> Blogs { get; set; }
        public List<GetWishlistDto> Wishlists { get; set; }
        public List<GetBasketDto> Baskets { get; set; }
        public List<GetAdressDto> Adresses { get; set; }
        public List<GetCheckDto> Checkes { get; set; }
        public ICollection<GetMessageDto> SentMessages { get; set; }
        public ICollection<GetMessageDto> ReceivedMessages { get; set; }

        public GetUserDto()
		{
            Products = new();
		}
	}
}

