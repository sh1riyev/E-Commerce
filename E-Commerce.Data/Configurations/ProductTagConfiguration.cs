using System;
using E_Commerce.Core.Entities;
using E_Commerce.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Web_Api.Data.Configurations
{
	public class ProductTagConfiguration: BaseEntityConfiguration<ProductTag>
    {
		public ProductTagConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            base.Configure(builder);
        }
    }
}

