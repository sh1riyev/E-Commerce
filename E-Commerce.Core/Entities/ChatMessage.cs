using System;
namespace E_Commerce.Core.Entities
{
	public class ChatMessage : BaseEntity
	{
        public string Message { get; set; }
        public string FromUserId { get; set; }
        public AppUser FromUser { get; set; }
        public string ToUserId { get; set; }
        public AppUser ToUser { get; set; }
        public bool IsSeen { get; set; }
        public bool IsImage { get; set; }
        public string? PublicId { get; set; }
        public ChatMessage()
		{
		}
	}
}

