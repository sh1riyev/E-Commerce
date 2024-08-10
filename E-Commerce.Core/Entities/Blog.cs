using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class Blog : BaseEntity
	{
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<BlogTags> BlogTags { get; set; }
        public List<BlogComment> BlogComments { get; set; }
        public Blog()
        {
            BlogTags = new();
        }
    }
}

