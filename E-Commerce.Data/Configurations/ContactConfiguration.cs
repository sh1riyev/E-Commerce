using System;
using E_Commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
	public class ContactConfiguration:BaseEntityConfiguration<Contact>
	{
		public ContactConfiguration()
		{
		}
        public override void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(100);
            builder.HasCheckConstraint("CK_Contact_Name_MinLength", "LEN(Name) >= 3");
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.HasCheckConstraint("CK_Contact_Email_MinLength", "LEN(Email) >= 3");
            builder.Property(c => c.Subject).HasMaxLength(100);
            builder.HasCheckConstraint("CK_Contact_Subject_MinLength", "LEN(Subject) >= 3");
            builder.Property(c => c.Message).HasMaxLength(300);
            builder.HasCheckConstraint("CK_Contact_Message_MinLength", "LEN(Message) >= 5");
            base.Configure(builder);
        }
    }
}

