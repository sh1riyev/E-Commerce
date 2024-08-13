using System;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.BlogCommentDto
{
	public class GetBlogCommentByAdminDto
	{
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string UserId { get; set; }
        public string AppUserFullName { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public GetBlogCommentByAdminDto()
		{
		}
	}
}

