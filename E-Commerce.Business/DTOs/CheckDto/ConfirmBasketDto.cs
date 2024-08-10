using System;
namespace E_Commerce.Business.DTOs.CheckDto
{
	public class ConfirmBasketDto
	{
        public string UserId { get; set; }
        public string AdressId { get; set; }
        public string? SaleCode { get; set; }
		public string SesionId { get; set; }
        public ConfirmBasketDto()
		{
		}
	}
}

