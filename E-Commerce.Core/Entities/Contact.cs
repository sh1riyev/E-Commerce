using System;
namespace E_Commerce.Core.Entities
{
	public class Contact : BaseEntity
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public Nullable<DateTime> RespondedAt { get; set; }
        public bool IsResponded { get; set; }
        public Contact()
		{
		}
	}
}

