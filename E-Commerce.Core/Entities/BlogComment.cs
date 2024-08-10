using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class BlogComment : BaseEntity
	{
        [ForeignKey(nameof(Blog))]
        public string BlogId { get; set; }
        public Blog Blog { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Content { get; set; }
        [ForeignKey(nameof(BlogComment))]
        public string? ParentId { get; set; }
        public BlogComment Parent { get; set; }
        public List<BlogComment> SubComments { get; set; }
        public BlogComment()
		{
		}
	}
}

