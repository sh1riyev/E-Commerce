using System;
using AutoMapper;
using E_Commerce.Business.DTOs.AdressDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class AdressProfile:Profile
	{
		public AdressProfile()
		{
			CreateMap<Adress, CreateAdressDto>().ReverseMap();
			CreateMap<Adress, UpdateAdressDto>().ReverseMap();
			CreateMap<Adress, GetAdressByAdmin>().ReverseMap();
			CreateMap<Adress, GetAdressDto>().ReverseMap();
        }
	}
}

