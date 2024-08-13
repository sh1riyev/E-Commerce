using System;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.BlogCommentDto
{ 
	public class GetBlogCommentDto
	{
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string UserId { get; set; }
        public string AppUserFullName { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public string? ParentId { get; set; }
        public GetBlogCommentDto Parent { get; set; }
        public GetBlogCommentDto()
		{
		}
	}
}

