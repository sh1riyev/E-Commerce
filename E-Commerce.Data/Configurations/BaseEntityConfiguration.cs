using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
        public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
        {
            public BaseEntityConfiguration()
            {
            }

            public virtual void Configure(EntityTypeBuilder<T> builder)
            {
                builder.Property(c => c.AddedBy)
                  .IsRequired()
                  .HasDefaultValue("System");
                builder.Property(c => c.CreatedAt)
                    .HasDefaultValue(DateTime.UtcNow.AddHours(8));
            }
        }
}

