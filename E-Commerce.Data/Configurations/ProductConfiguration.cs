using System;
using System.Reflection.Emit;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class ProductConfiguration:BaseEntityConfiguration<Product>
	{
		public ProductConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Size).HasDefaultValue("Standart");
            builder.Property(p => p.ReviewCount).HasDefaultValue(0);
            builder.HasCheckConstraint("CK_Product_Name_MinLength", "LEN(Name) >= 3");
            builder.HasCheckConstraint("CK_Product_Size_MinLength", "LEN(Size) >= 1 AND LEN(Size)<40");
            builder.HasCheckConstraint("CK_Product_Color_MinLength", "LEN(Color) >= 3");
            builder.HasCheckConstraint("CK_Product_Material_MinLength", "LEN(Material) >= 3");
            builder.HasCheckConstraint("CK_Product_Content_MinLength", "LEN(Content) >= 10");
            builder.HasCheckConstraint("CK_Product_ProductCode_MinLength", "LEN(ProductCode) >= 5");
            builder.HasCheckConstraint("CK_Product_Price_MinLength", "[Price] >= 0");
            builder.HasCheckConstraint("CK_Product_SalePercentage", "[SalePercentage] >= 0 AND [SalePercentage] <= 100");
            builder.HasCheckConstraint("CK_Product_StarsCount", "[StarsCount] >= 0 AND [StarsCount] <= 5");
            builder.HasCheckConstraint("CK_Product_Tax_MinLength", "[Tax] >= 0");
            builder.HasCheckConstraint("CK_Product_Weight_MinLength", "[Weight] >= 0");
            builder.HasCheckConstraint("CK_Product_Count_MinLength", "[Count] >= 0");
            builder.HasCheckConstraint("CK_Product_VipDegre_MinLength", "[VipDegre] >= 0");
            builder.Property(p => p.Count).HasDefaultValue(0);
            builder.Property(p => p.StarsCount).HasDefaultValue(5);
            base.Configure(builder);
        }
    }
}

