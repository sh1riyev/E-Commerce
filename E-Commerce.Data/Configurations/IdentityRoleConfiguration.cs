using System;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class IdentityRoleConfiguration:IEntityTypeConfiguration<IdentityRole>
	{
		public IdentityRoleConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            string AdminRoleId = "20cc338d-4993-4a04-87b5-0f8fc208ceba";
            string SupperAdminRoleId = "fee579ed-a458-41ad-b9f4-4299f5a50029";
            string UserRoleId = "03ae8e72-74d1-4479-b8a3-d628cebf7309";
            string SellerRoleId = "e535ff1b-18f2-4ea5-98e0-467b088d5a27";
            builder.HasData(new IdentityRole
            {
                Id = AdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
           new IdentityRole
           {
               Id = SupperAdminRoleId,
               Name = "SupperAdmin",
               NormalizedName = "SUPPERADMIN"
           },
           new IdentityRole
           {
               Id = UserRoleId,
               Name = "User",
               NormalizedName = "USER"
           },
           new IdentityRole
           {
               Id = SellerRoleId,
               Name = "Seller",
               NormalizedName = "SELLER"
           });
        }
    }
}

