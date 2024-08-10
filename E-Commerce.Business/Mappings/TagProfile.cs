using System;
using AutoMapper;
using E_Commerce.Business.DTOs.TagDto;
using	E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class TagProfile:Profile
	{
		public TagProfile()
		{
			CreateMap<Tag, CreateTagDto>().ReverseMap();
			CreateMap<Tag, UpdateTagDto>().ReverseMap();
			CreateMap<Tag, GetTagByAdminDto>().ReverseMap();
			CreateMap<Tag, GetTagDto>().ReverseMap();
        }
	}
}

