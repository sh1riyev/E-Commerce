using System;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class AppUserConfigruation : IEntityTypeConfiguration<AppUser>
	{
		public AppUserConfigruation()
		{
		}

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            string AdminId = "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2";
            string SupperAdminId = "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd";
            builder.Property(a => a.AddedBy).HasDefaultValue("System");
            builder.Property(a => a.CreatedAt)
               .HasDefaultValue(DateTime.UtcNow.AddHours(4));
            builder.Property(a => a.FullName).HasMaxLength(100);
            builder.Property(a => a.UserName).HasMaxLength(100);
            builder.Property(a => a.Email).HasMaxLength(100);

            AppUser Admin = new AppUser
            {
                Id = AdminId,
                Email = "isiriyev@gmail.com",
                NormalizedEmail = "ISIRIYEV@GMAIL.COM",
                NormalizedUserName = "SHIRIYEV",
                UserName = "Shiriyev",
                EmailConfirmed = true,
                IsActive = true,
                IsSeller = true,
                FullName = "Ilgar Shiriyev",
                PhoneNumber = "+994508802323",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.Now,
                AddedBy = "System",

            };
            AppUser SupperAdmin = new AppUser
            {
                Id = SupperAdminId,
                Email = "siriyev@hotmail.com",
                NormalizedEmail = "SIRIYEV@HOTMAIL.COM",
                NormalizedUserName = "ILGAR023",
                UserName = "Ilgar23",
                EmailConfirmed = true,
                IsActive = true,
                IsSeller = true,
                FullName = "Rufat Code",
                PhoneNumber = "+994508802323",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.Now,
                AddedBy = "System"

            };
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
            string AdminPassword = hasher.HashPassword(Admin, "Ilgar123.");
            string SupperAdminPassword = hasher.HashPassword(SupperAdmin, "Ilgar123.");
            Admin.PasswordHash = AdminPassword;
            SupperAdmin.PasswordHash = SupperAdminPassword;
            builder.HasData(Admin);
            builder.HasData(SupperAdmin);
        }
    }
}

