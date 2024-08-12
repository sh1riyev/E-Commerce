using System;
using AutoMapper;
using E_Commerce.Business.DTOs.CheckDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class CheckProfile:Profile
	{
		public CheckProfile()
		{
			CreateMap<Check, GetCheckDto>().ReverseMap();
            CreateMap<Check, UpdateCheckDto>().ReverseMap();
            CreateMap<CheckProduct, GetCheckProductDto>().ReverseMap();
            CreateMap<Check, GetCheckByAdminDto>().ReverseMap();
        }
	}
}

