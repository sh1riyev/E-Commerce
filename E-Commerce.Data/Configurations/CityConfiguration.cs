using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Configurations
{
	public class CityConfiguration:BaseEntityConfiguration<City>
	{
		public CityConfiguration()
		{
		}
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<City> builder)
        {
            builder.HasCheckConstraint("CK_City_Name_MinLength", "LEN(Name) >= 3");
            builder.HasCheckConstraint("CK_City_DeliverPrice_MinLength", "[DeliverPrice] >= 0");
            builder.Property(a => a.Name).HasMaxLength(100);
            base.Configure(builder);
        }
    }
}

