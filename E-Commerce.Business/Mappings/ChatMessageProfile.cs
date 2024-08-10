using System;
using AutoMapper;
using E_Commerce.Business.DTOs.ChatMessageDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class ChatMessageProfile : Profile
	{
		public ChatMessageProfile()
		{
            CreateMap<ChatMessage, SendMessageDto>().ReverseMap();
            CreateMap<ChatMessage, GetMessageByAdmin>().ReverseMap();
            CreateMap<ChatMessage, GetMessageDto>().ReverseMap();
            CreateMap<ChatMessage, UpdateMessageDto>().ReverseMap();
        }
	}
}

