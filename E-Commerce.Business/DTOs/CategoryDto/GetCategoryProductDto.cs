using System;

namespace E_Commerce.Business.DTOs.CategoryDto
{
	public class GetCategoryProductDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string ProductCode { get; set; }
        public GetCategoryProductDto()
		{
		}
	}
}

