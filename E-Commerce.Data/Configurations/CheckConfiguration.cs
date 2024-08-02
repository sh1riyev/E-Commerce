using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class CheckConfiguration : BaseEntityConfiguration<Check>
    {

		public CheckConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Check> builder)
        {
            builder.HasCheckConstraint("CK_Check_TotalAmmount_MinLength", "[TotalAmmount] >= 0");
            builder.HasCheckConstraint("CK_Check_Sale_MinLength", "[Sale] >= 0  AND [Sale]  <= 100");
            builder.HasCheckConstraint("CK_Check_Status_MinLength", "[Status] >= 0");
            builder.Property(c => c.Status).HasDefaultValue(1);
            builder.Property(c => c.Status).HasMaxLength(100);
            builder.Property(c => c.Promocode).HasMaxLength(100);
            builder
             .HasOne(c => c.AppUser)
             .WithMany(u => u.Checkes)
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }

    }
}

