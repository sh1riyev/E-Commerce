using System;
using AutoMapper;
using	E_Commerce.Business.DTOs.CompaignsDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class CompaignsProfile:Profile
	{
		public CompaignsProfile()
		{
			CreateMap<Campaign, CreateCompaignsDto>().ReverseMap();
			CreateMap<Campaign, UpdateCompaignsDto>().ReverseMap();
			CreateMap<Campaign, GetCompaignsByAdminDto>().ReverseMap();
			CreateMap<Campaign, GetCompaignsDto>().ReverseMap();
        }
	}
}

