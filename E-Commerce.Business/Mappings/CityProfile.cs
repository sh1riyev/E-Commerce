using System;
using AutoMapper;
using	E_Commerce.Business.DTOs.CityDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class CityProfile:Profile
	{
		public CityProfile()
		{
			CreateMap<City, CreateCityDto>().ReverseMap();
			CreateMap<City, UpdateCityDto>().ReverseMap();
			CreateMap<City, GetCityByAdminDto>().ReverseMap();
			CreateMap<City, GetCityDto>().ReverseMap();
        }
	}
}

