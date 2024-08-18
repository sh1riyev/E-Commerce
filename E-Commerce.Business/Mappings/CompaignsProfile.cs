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
			CreateMap<Compaigns, CreateCompaignsDto>().ReverseMap();
			CreateMap<Compaigns, UpdateCompaignsDto>().ReverseMap();
			CreateMap<Compaigns, GetCompaignsByAdminDto>().ReverseMap();
			CreateMap<Compaigns, GetCompaignsDto>().ReverseMap();
        }
	}
}

