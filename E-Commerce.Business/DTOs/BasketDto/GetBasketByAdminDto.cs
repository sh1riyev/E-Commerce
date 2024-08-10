using System;
using E_Commerce.Business.DTOs.BasketDto;

namespace E_Commerce.Business.DTOs.BasketDto
{
	public class GetBasketByAdminDto
	{
        public string Id { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetBasketProductDto Product { get; set; }
        public string AppUserUserName { get; set; }
        public string UserId { get; set; }
        public string AddedBy { get; set; }
        public GetBasketByAdminDto()
		{
		}
	}
}

