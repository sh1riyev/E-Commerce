using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Commerce.Data.Configurations
{
	public class SettingConfiguration:BaseEntityConfiguration<Setting>
	{
		public SettingConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(s => s.Key).HasMaxLength(100);
            builder.Property(s => s.Value).HasMaxLength(300);
            builder.HasCheckConstraint("CK_Setting_Value_MinLength", "LEN(Value) >= 3");
            builder.HasData(new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Free Shipping",
                Value = "Free shipping on all US order or order above $100"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Shop with Confidence",
                Value = "Our Protection covers your purchase from click to delivery"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "24/7 Help Center",
                Value = "Round-the-clock assistance for a smooth shopping experience"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Location",
                Value = "Neftchi Gurban 168, Baku 1001"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Phone",
                Value = "(+0) 900 901 904"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key="Email",
                Value= "isiriyev@gmail.com"
            },new Setting
            {
                Id=Guid.NewGuid().ToString(),
                Key="Facebook",
                Value="www.facebook.com"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "LinkedIn",
                Value = "linkedin.com"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Instagram",
                Value = "www.instagram.com"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Google",
                Value = "www.google.com"
            }, new Setting
            {
                Id = Guid.NewGuid().ToString(),
                Key = "Youtube",
                Value = "www.youtube.com"
            }) ;
            base.Configure(builder);
        }
    }
}

