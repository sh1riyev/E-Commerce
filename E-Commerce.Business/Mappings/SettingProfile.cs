using System;
using AutoMapper;
using E_Commerce.Business.DTOs.SettingDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class SettingProfile:Profile
	{
		public SettingProfile()
		{
			CreateMap<Setting, CreateSettingDto>().ReverseMap();
			CreateMap<Setting, UpdateSettingDto>().ReverseMap();
			CreateMap<Setting, GetSettingByAdminDto>().ReverseMap();
			CreateMap<Setting, GetSettingDto>().ReverseMap();
        }
    }
}

