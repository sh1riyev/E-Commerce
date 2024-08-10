using System;

namespace E_Commerce.Business.DTOs.BasketDto
{
	public class GetBasketDto
	{
        public string Id { get; set; }
        public int Count { get; set; }
        public GetBasketProductDto Product { get; set; }
        public string AppUserUserName { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public GetBasketDto()
		{
		}
	}
}

