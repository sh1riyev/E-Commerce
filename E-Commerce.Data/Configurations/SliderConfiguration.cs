using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class SliderConfiguration: BaseEntityConfiguration<Slider>
    {
		public SliderConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(s => s.Title).HasMaxLength(100);
            builder.Property(s => s.Information).HasMaxLength(100);
            builder.HasCheckConstraint("CK_Slider_Title_MinLength", "LEN(Title) >= 3");
            builder.HasCheckConstraint("CK_Slider_Information_MinLength", "LEN(Information) >= 3");
            builder.HasCheckConstraint("CK_Slider_Description_MinLength", "LEN(Description) >= 10");
            builder.HasCheckConstraint("CK_Slider_Content_MinLength", "LEN(Content) >= 10");
            base.Configure(builder);
        }
    }
}

