using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class Category : BaseEntity
	{
        public string Name { get; set; }
        [ForeignKey(nameof(Category))]
        public string? ParentId { get; set; }
        public Category Parent { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Product> Products { get; set; }
        public bool IsMain { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public Category()
        {

        }
    }
}

