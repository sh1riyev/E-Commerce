using System;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Data
{
	public class DataContext : IdentityDbContext <AppUser>
	{
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DataContext(DbContextOptions context) : base(context)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);


            base.OnModelCreating(modelBuilder);
        }
    }
}

