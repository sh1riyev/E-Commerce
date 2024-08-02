using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class BlogCommentConfiguration : BaseEntityConfiguration<BlogComment>
    {
        public override void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.HasOne(pc => pc.Blog)
            .WithMany(p => p.BlogComments)
            .HasForeignKey(pc => pc.BlogId)
            .OnDelete(DeleteBehavior.NoAction);
            base.Configure(builder);
        }
    }
}

