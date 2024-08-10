using System;
namespace E_Commerce.Core.Entities
{
	public class Brand : BaseEntity
	{
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string PublicId { get; set; }
        public List<Product> Products { get; set; }
        public Brand()
		{
		}
	}
}

