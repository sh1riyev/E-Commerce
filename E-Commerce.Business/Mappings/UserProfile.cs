using System;
using AutoMapper;
using   E_Commerce.Business.DTOs.UserDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class UserProfile:Profile
	{
		public UserProfile()
		{
            CreateMap<AppUser, GetUserDto>().ReverseMap();
            CreateMap<GetUserDetailDto, GetUserDto>()
                .ReverseMap()
                .ForMember(u => u.WishlistsCount, map => map.MapFrom(p => p.Wishlists.FindAll(w => !w.IsDeleted).Count))
                .ForMember(c => c.ProductsCount, map => map.MapFrom(p => p.Products.FindAll(c => !c.IsDeleted).Count))
                .ForMember(c => c.BlogsCount, map => map.MapFrom(p => p.Blogs.FindAll(c => !c.IsDeleted).Count));
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
            CreateMap<Product, GetUserProduct>().ReverseMap();
            CreateMap<ProductTag, GetUserProductTags>().ReverseMap();
            CreateMap<GetUserProductImage, ProductImage>().ReverseMap();
        }
	}
}

