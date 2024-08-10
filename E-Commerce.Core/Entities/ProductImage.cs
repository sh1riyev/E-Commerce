using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class ProductImage : BaseEntity
	{
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public ProductImage()
		{
		}
	}
}

