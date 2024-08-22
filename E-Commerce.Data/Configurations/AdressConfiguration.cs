using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class AdressConfiguration: BaseEntityConfiguration<Adress>
    {
		public AdressConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder.HasCheckConstraint("CK_Adress_LocationName_MinLength", "LEN(LocationName) >= 3");
            builder.HasCheckConstraint("CK_Adress_Street_MinLength", "LEN(Street) >= 3");
            builder.HasCheckConstraint("CK_Adress_State_MinLength", "LEN(State) >= 3");
            builder.HasCheckConstraint("CK_Adress_ZipCode_MinLength", "LEN(ZipCode) >= 3");
            builder.Property(a => a.Street).HasMaxLength(100);
            builder.Property(a => a.LocationName).HasMaxLength(100);
            builder.Property(a => a.State).HasMaxLength(100);
            builder.Property(a => a.ZipCode).HasMaxLength(100);
            base.Configure(builder);
        }
    }
}

