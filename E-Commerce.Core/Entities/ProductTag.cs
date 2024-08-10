using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class ProductTag : BaseEntity
	{
        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(Tag))]
        public string TagId { get; set; }
        public Tag Tag { get; set; }
        public ProductTag()
		{
		}
	}
}

