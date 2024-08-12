using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.SliderDto
{
	public class CreateSliderDto
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public IFormFile Image { get; set; }
        public CreateSliderDto()
		{
		}
	}
}

