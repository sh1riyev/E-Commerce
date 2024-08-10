using System;
using AutoMapper;
using E_Commerce.Business.DTOs.AccountDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class AccountProfile : Profile
	{
        public AccountProfile()
        {
            CreateMap<AppUser, UserRegisterDto>().ReverseMap();
            CreateMap<AppUser, UserLoginDto>().ReverseMap();
            CreateMap<AppUser, UserUpdateDto>().ReverseMap();
        }
       
    }
}

