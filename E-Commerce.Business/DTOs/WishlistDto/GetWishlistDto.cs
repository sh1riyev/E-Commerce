using System;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Business.DTOs.ProductDto;

namespace E_Commerce.Business.DTOs.WishlistDto
{
	public class GetWishlistDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public GetProductDto Product { get; set; }
        public string AppUserUserName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetWishlistDto()
		{
		}
	}
}

