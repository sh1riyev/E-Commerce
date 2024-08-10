using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class Product : BaseEntity
	{
        public string Name { get; set; }
        public double Price { get; set; }
        public double SalePercentage { get; set; }
        public int StarsCount { get; set; }
        public double Tax { get; set; }
        public int Count { get; set; }
        public string Content { get; set; }
        public string ProductCode { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public int ReviewCount { get; set; }
        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Brand))]
        public string BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string SellerId { get; set; }
        public AppUser Seller { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<Wishlist> Wishlists { get; set; }
        public List<Basket> Baskets { get; set; }
        public List<CheckProduct> CheckProducts { get; set; }

        public bool IsAccepted { get; set; }
        public Nullable<DateTime> AcceptedDate { get; set; }
        public bool IsVIP { get; set; }
        public int VipDegre { get; set; }
        public Nullable<DateTime> VipDate { get; set; }
        public bool IsDonation { get; set; }
        public Product()
        {
            ProductImages = new();
            ProductTags = new();
        }
    }
}

