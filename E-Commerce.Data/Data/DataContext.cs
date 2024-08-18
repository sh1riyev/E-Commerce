using System;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Data
{
	public class DataContext : IdentityDbContext <AppUser>
	{
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTags> BlogTags { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Compaigns> Compaigns { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<CheckProduct> CheckProducts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
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

