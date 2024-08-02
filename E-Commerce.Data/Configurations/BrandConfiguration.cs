using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class BrandConfiguration:BaseEntityConfiguration<Brand>
	{
		public BrandConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.HasCheckConstraint("CK_Brand_Name_MinLength", "LEN(Name) >= 3");
            base.Configure(builder);
        }
    }
}

