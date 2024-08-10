using System;

namespace E_Commerce.Business.DTOs.ChatMessageDto
{
	public class GetMessageDto
	{
        public string Id { get; set; }
        public string Message { get; set; }
        public string FromUserId { get; set; }
        public string FromUserUserName { get; set; }
        public string? FromUserProfileImageUrl { get; set; }
        public string ToUserId { get; set; }
        public string ToUserUserName { get; set; }
        public string ToUserProfileImageUrl { get; set; }
        public bool IsSeen { get; set; }
        public bool IsImage { get; set; }
        public string? PublicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetMessageDto()
		{
		}
	}
}

