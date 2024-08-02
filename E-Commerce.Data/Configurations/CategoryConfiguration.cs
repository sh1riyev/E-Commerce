using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class CategoryConfiguration: BaseEntityConfiguration<Category>
    {
		public CategoryConfiguration()
		{
		}

        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
               .HasMaxLength(100);
            builder.HasCheckConstraint("CK_Category_Name_MinLength", "LEN(Name) >= 3");
            base.Configure(builder);
        }

    }
}

