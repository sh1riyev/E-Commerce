using System;
using E_Commerce.Business.DTOs.ProductDto;

namespace E_Commerce.Business.DTOs.WishlistDto
{
	public class GetWishlistByAdmin
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetProductDto Product { get; set; }
        public string AppUserUserName { get; set; }
        public string UserId { get; set; }
        public string AddedBy { get; set; }
        public GetWishlistByAdmin()
		{
		}
	}
}

