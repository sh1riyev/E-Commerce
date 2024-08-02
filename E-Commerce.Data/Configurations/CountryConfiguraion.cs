using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class CountryConfiguraion: BaseEntityConfiguration<Country>
    {
		public CountryConfiguraion()
		{
		}
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasCheckConstraint("CK_Country_Name_MinLength", "LEN(Name) >= 3");
            builder.Property(a => a.Name).HasMaxLength(100);
            base.Configure(builder);
        }
    }
}

