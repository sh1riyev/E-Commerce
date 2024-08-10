using System;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.BlogDto
{
	public class GetBlogComments
	{
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string AppUserFullName { get; set; }
        public string AppUserProfileImageUrl { get; set; }
        public string Content { get; set; }
        public string? ParentId { get; set; }
        public GetBlogComments Parent { get; set; }
        public GetBlogComments()
		{
		}
	}
}

