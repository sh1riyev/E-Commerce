using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class BlogTags : BaseEntity
	{
        [ForeignKey(nameof(Tag))]
        public string TagId { get; set; }
        public Tag Tag { get; set; }
        [ForeignKey(nameof(Blog))]
        public string BlogId { get; set; }
        public Blog Blog { get; set; }
        public BlogTags()
		{
		}
	}
}

