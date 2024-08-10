using System;
using AutoMapper;
using E_Commerce.Business.DTOs.CountryDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class CountryProfile:Profile
	{
		public CountryProfile()
		{
			CreateMap<Country, CreateCountryDto>().ReverseMap();
			CreateMap<Country, UpdateCountryDto>().ReverseMap();
			CreateMap<Country, GetCountryByAdminDto>().ReverseMap();
			CreateMap<Country, GetCountryDto>().ReverseMap();
        }
	}
}

