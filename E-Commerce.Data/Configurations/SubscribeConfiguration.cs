using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using E_Commerce.Core.Entities;

namespace E_Commerce.Data.Configurations
{
	public class SubscribeConfiguration:BaseEntityConfiguration<Subscribe>
	{
		public SubscribeConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.Property(s => s.Email).HasMaxLength(100);
            builder.Property(s => s.Gender).HasMaxLength(100);
            builder.HasCheckConstraint("CK_Subscribe_Email_MinLength", "LEN(Email) >= 3");
            builder.HasCheckConstraint("CK_Subscribe_Gender_MinLength", "LEN(Gender) >= 3");
            base.Configure(builder);
        }
    }
}

