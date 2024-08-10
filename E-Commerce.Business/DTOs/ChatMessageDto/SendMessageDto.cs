using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.ChatMessageDto
{
	public class SendMessageDto
	{
        public string Message { get; set; }
        public bool IsImage { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public SendMessageDto()
		{
		}
	}
}

