using System;
using E_Commerce.Core.Entities;
using E_Commerce.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class CompaignsConfiguration: BaseEntityConfiguration<Compaigns>
    {
		public CompaignsConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Compaigns> builder)
        {
            builder.Property(c => c.Sale).HasDefaultValue(0);
            builder.HasCheckConstraint("CK_Configure_Headling_MinLength", "LEN(Headling) >= 3 AND LEN(Headling)  <= 100");
            builder.HasCheckConstraint("CK_Configure_Content_MinLength", "LEN(Content) >= 10");
            builder.HasCheckConstraint("CK_Configure_Info_MinLength", "LEN(Info) >= 3 AND LEN(Info)  <= 100");
            builder.HasCheckConstraint("CK_Configure_Sale", "[Sale] >= 0 AND [Sale]  <= 100");
            base.Configure(builder);
        }
    }
}

