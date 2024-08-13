using System;
using AutoMapper;
using E_Commerce.Business.DTOs.SubscribeDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class SubscribeProfile:Profile
	{
		public SubscribeProfile()
		{
			CreateMap<CreateSubscribeDto, Subscribe>().ReverseMap();
			CreateMap<UpdateSubscribeDto, Subscribe>().ReverseMap();
			CreateMap<GetSubscribeByAdminDto, Subscribe>().ReverseMap();
			CreateMap<GetSubscribeDto, Subscribe>().ReverseMap();
        }
	}
}

