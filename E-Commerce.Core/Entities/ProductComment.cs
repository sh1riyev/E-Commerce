using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class ProductComment : BaseEntity
	{
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public ProductComment()
		{
		}
	}
}

