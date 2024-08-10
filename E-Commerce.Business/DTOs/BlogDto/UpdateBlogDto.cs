using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.BlogDto
{
	public class UpdateBlogDto
	{
        public string Id { get; set; }
        public IFormFile? Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public List<string>? TagIds { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateBlogDto()
		{
		}
	}
}

