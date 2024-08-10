using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.ProductDto
{
	public class UpdateProductDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double SalePercentage { get; set; }
        public double Tax { get; set; }
        public int Count { get; set; }
        public string Content { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; }
        public string Material { get; set; }
        public string CategoryId { get; set; }
        public string BrandId { get; set; }
        public string SellerId { get; set; }
        public IFormFile[]? Images { get; set; }
        public List<string>? TagIds { get; set; }
        public bool IsDeleted { get; set; }

        //inovations
        public bool IsAccepted { get; set; }
        public bool IsVIP { get; set; }
        public int VipDegre { get; set; }
        public bool IsDonation { get; set; }
        public UpdateProductDto()
		{
		}
	}
}

