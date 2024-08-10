using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.BrandDto
{
	public class UpdateBrandDto
	{
        public string Id { get; set; }
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateBrandDto()
		{
		}
	}
}

