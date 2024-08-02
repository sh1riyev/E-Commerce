using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_Commerce.Core.Entities;

namespace E_Commerce.Data.Configurations
{
	public class WishlistConfiguration: BaseEntityConfiguration<Wishlist>
    {
		public WishlistConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder
           .HasOne(w => w.AppUser)
           .WithMany(u => u.Wishlists)
           .HasForeignKey(m => m.UserId)
           .OnDelete(DeleteBehavior.Restrict);
            builder
           .HasOne(w => w.Product)
           .WithMany(u => u.Wishlists)
           .HasForeignKey(m => m.ProductId)
           .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}

