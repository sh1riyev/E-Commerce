using System;
namespace E_Commerce.Business.DTOs.BlogCommentDto
{
	public class CreateBlogCommentDto
	{
        public string BlogId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string? ParentId { get; set; }
        public CreateBlogCommentDto()
		{
		}
	}
}

