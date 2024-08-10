using System;
using AutoMapper;
using E_Commerce.Business.DTOs.AdressDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class AddressProfile:Profile
	{
		public AddressProfile()
		{
			CreateMap<Address, CreateAdressDto>().ReverseMap();
			CreateMap<Address, UpdateAdressDto>().ReverseMap();
			CreateMap<Address, GetAdressByAdmin>().ReverseMap();
			CreateMap<Address, GetAdressDto>().ReverseMap();
        }
	}
}

