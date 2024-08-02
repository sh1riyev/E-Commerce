using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class BasketConfiguration: BaseEntityConfiguration<Basket>
    {
		public BasketConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasCheckConstraint("CK_Basket_Count_MinLength", "[Count] >= 0");
            builder
          .HasOne(w => w.AppUser)
          .WithMany(u => u.Baskets)
          .HasForeignKey(m => m.UserId)
          .OnDelete(DeleteBehavior.Restrict);
            builder
           .HasOne(w => w.Product)
           .WithMany(u => u.Baskets)
           .HasForeignKey(m => m.ProductId)
           .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}

