using System;
namespace E_Commerce.Business.DTOs.CheckDto
{
	public class CheckOutDto
	{
		public string UserId { get; set; }
		public string AdressId { get; set; }
		public string? SaleCode { get; set; }

        public CheckOutDto()
		{
		}
	}
}

