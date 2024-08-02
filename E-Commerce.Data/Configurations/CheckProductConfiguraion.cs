using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class CheckProductConfiguraion: BaseEntityConfiguration<CheckProduct>
    {
		public CheckProductConfiguraion()
		{
		}
        public override void Configure(EntityTypeBuilder<CheckProduct> builder)
        {
            builder.HasCheckConstraint("CK_CheckProduct_Price_MinLength", "[Price] >= 0");
            builder.HasCheckConstraint("CK_CheckProduct_ProductCount_MinLength", "[ProductCount] >= 0");
            builder.HasOne(cp => cp.Check)
           .WithMany(p => p.CheckProducts)
           .HasForeignKey(pc => pc.CheckId)
           .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(cp => cp.Product)
           .WithMany(p => p.CheckProducts)
           .HasForeignKey(pc => pc.ProductId)
           .OnDelete(DeleteBehavior.NoAction);
            base.Configure(builder);
        }
    }
}

