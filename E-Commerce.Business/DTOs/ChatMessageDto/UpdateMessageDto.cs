using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.ChatMessageDto
{
	public class UpdateMessageDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Message { get; set; }
        public bool IsImage { get; set; }
        public UpdateMessageDto()
		{
		}
	}
}

