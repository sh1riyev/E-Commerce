using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_Commerce.Core.Entities;

namespace E_Commerce.Data.Configurations
{
	public class TagConfiguration: BaseEntityConfiguration<Tag>
    {
		public TagConfiguration()
		{
		}

        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.HasCheckConstraint("CK_Tag_Name_MinLength", "LEN(Name) >= 3");
            base.Configure(builder);
        }
    }
}

