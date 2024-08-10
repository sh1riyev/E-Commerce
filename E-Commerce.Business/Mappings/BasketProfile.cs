using System;
using AutoMapper;
using E_Commerce.Business.DTOs.BasketDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class BasketProfile:Profile
	{
		public BasketProfile()
		{
			CreateMap<Basket, BasketDto>().ReverseMap();
			CreateMap<Basket, UpdateBasketDto>().ReverseMap();
			CreateMap< GetBasketDto,Basket>().ReverseMap();
			CreateMap<GetBasketByAdminDto,Basket>().ReverseMap();
			CreateMap<GetBasketProductDto, Product>()
				.ReverseMap()
				.ForMember(c => c.Image, map => map.MapFrom(p => p.ProductImages[0].ImageUrl));
        }
	}
}

