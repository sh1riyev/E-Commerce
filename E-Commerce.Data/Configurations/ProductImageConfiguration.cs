using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class ProductImageConfiguration:BaseEntityConfiguration<ProductImage>
	{
		public ProductImageConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            base.Configure(builder);
        }
    }
}

