using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class BlogTagConfiguration: BaseEntityConfiguration<BlogTags>
    {
		public BlogTagConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<BlogTags> builder)
        {
            base.Configure(builder);
        }
    }
}

