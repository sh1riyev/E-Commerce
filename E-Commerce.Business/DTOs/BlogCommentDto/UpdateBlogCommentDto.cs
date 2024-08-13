using System;
namespace E_Commerce.Business.DTOs.BlogCommentDto
{
	public class UpdateBlogCommentDto
	{
        public string Id { get; set; }
        public string BlogId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateBlogCommentDto()
		{
		}
	}
}

