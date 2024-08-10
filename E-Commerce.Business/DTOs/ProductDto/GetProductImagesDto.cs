using System;
namespace E_Commerce.Business.DTOs.ProductDto
{
	public class GetProductImagesDto
	{
		public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public GetProductImagesDto()
		{
		}
	}
}

