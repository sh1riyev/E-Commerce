using System;
namespace E_Commerce.Business.DTOs.SliderDto
{
	public class GetSliderDto
	{
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public string ImageUrl { get; set; }
        public GetSliderDto()
		{
		}
	}
}

