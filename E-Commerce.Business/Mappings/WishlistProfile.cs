using System;
using AutoMapper;
using E_Commerce.Business.DTOs.WishlistDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class WishlistProfile:Profile
	{
		public WishlistProfile()
		{
			CreateMap<Wishlist, CreateWishlistDto>().ReverseMap();
			CreateMap<Wishlist, GetWishlistByAdmin>().ReverseMap();
			CreateMap<Wishlist, GetWishlistDto>().ReverseMap();
        }
	}
}

