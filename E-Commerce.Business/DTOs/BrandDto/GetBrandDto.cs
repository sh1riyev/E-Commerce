using System;

namespace E_Commerce.Business.DTOs.BrandDto
{
	public class GetBrandDto
	{
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public List<GetBrandProductDto> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetBrandDto()
		{
		}
	}
}

