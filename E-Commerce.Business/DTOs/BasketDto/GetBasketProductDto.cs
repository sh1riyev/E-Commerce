using System;

namespace E_Commerce.Business.DTOs.BasketDto
{
	public class GetBasketProductDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double SalePercentage { get; set; }
        public string CategoryName { get; set; }
        public double Tax { get; set; }
        public string Content { get; set; }
        public string ProductCode { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public GetBasketProductDto()
		{
		}
	}
}

