using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.BrandDto
{
	public class CreateBrandDto
	{
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public CreateBrandDto()
		{
		}
	}
}

