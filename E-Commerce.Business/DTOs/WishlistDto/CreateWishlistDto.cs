using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Business.DTOs.WishlistDto
{
	public class CreateWishlistDto
	{
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public CreateWishlistDto()
		{
		}
	}
}

