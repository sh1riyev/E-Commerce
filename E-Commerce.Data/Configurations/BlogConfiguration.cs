using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class BlogConfiguration: BaseEntityConfiguration<Blog>
    {
		public BlogConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasCheckConstraint("CK_Blog_Title_MinLength", "LEN(Title) >= 3");
            builder.HasCheckConstraint("CK_Blog_Content_MinLength", "LEN(Content) >= 5");
            builder.HasCheckConstraint("CK_Blog_Information_MinLength", "LEN(Information) >= 5");
            builder.HasCheckConstraint("CK_Blog_Description_MinLength", "LEN(Description) >= 20");
            builder.HasCheckConstraint("CK_Blog_ViewCount", "[ViewCount] >= 0");
            base.Configure(builder);
        }
    }
}

