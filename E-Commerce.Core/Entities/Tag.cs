using System;
namespace E_Commerce.Core.Entities
{
	public class Tag  : BaseEntity
	{
        public string Name { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<BlogTags> BlogTags { get; set; }
        public Tag()
		{
		}
	}
}

