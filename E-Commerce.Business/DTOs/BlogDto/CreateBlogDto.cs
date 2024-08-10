using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.BlogDto
{
	public class CreateBlogDto
	{
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public List<string> TagIds { get; set; }
        public CreateBlogDto()
		{
            TagIds = new();
		}
	}
}

