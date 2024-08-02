using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class ChatMessageConfiguration : BaseEntityConfiguration<ChatMessage>
    {
        public ChatMessageConfiguration()
        {
        }
        public override void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasCheckConstraint("CK_ChatMessage_Message_MinLength", "LEN(Message) >= 1");
            builder
           .HasOne(m => m.ToUser)
           .WithMany(u => u.ReceivedMessages)
           .HasForeignKey(m => m.ToUserId)
           .OnDelete(DeleteBehavior.Restrict);
            builder
             .HasOne(m => m.FromUser)
             .WithMany(u => u.SentMessages)
             .HasForeignKey(m => m.FromUserId)
             .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}

