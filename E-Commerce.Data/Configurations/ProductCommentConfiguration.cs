using System;
using System.Reflection.Emit;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class ProductCommentConfiguration: BaseEntityConfiguration<ProductComment>
    {
		public ProductCommentConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.HasCheckConstraint("CK_ProductComment_Rating_MinLength", "[Rating] >= 0 AND [Rating] <=5");
            builder.HasOne(pc => pc.Product)
            .WithMany(p => p.ProductComments)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
            base.Configure(builder);
        }
    }
}

