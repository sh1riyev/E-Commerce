using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Business.DTOs.BlogDto
{
	public class GetBlogByAdminDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public int ViewCount { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string AppUserFullName { get; set; }
        public List<GetBlogTagDto> BlogTags { get; set; }
        public List<GetBlogComments> BlogComments { get; set; }
        public GetBlogByAdminDto()
		{
		}
	}
}

