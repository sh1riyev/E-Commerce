using System;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Business.DTOs.ProductDto;

namespace E_Commerce.Business.DTOs.CheckDto
{
	public class GetCheckProductDto
	{
        public string Id { get; set; }
        public string ProductId { get; set; }
        public GetProductDto Product { get; set; }
        public string CheckId { get; set; }
        public double Price { get; set; }
        public int ProductCount { get; set; }
        public GetCheckProductDto()
		{
		}
	}
}

