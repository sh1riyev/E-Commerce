using System;
using AutoMapper;
using E_Commerce.Business.DTOs.CategoryDto;
using E_Commerce.Business.DTOs.ProductDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class ProductProfile:Profile
	{
		public ProductProfile()
		{
			CreateMap<CreateProductDto, Product>().ReverseMap();
			CreateMap<UpdateProductDto, Product>().ReverseMap();
			CreateMap<GetProductDto,Product >()
				.ReverseMap()
				.ForMember(c=>c.WishlistCount,map=>map.MapFrom(p=>p.Wishlists.FindAll(w=>!w.IsDeleted).Count))
				.ForMember(c => c.CommentCount, map => map.MapFrom(p => p.ProductComments.FindAll(c=>!c.IsDeleted).Count));
			CreateMap<GetProductDetailDto,Product >()
				.ReverseMap()
				.ForMember(c=>c.WishlistCount,map=>map.MapFrom(p=>p.Wishlists.FindAll(w=>!w.IsDeleted).Count))
				.ForMember(c => c.CommentCount, map => map.MapFrom(p => p.ProductComments.FindAll(c=>!c.IsDeleted).Count));
            CreateMap<GetProductByAdminDto, Product>()
				.ReverseMap()
				.ForMember(c => c.WishlistCount, map => map.MapFrom(p => p.Wishlists.FindAll(w => !w.IsDeleted).Count))
				.ForMember(c => c.CommentCount, map => map.MapFrom(p => p.ProductComments.FindAll(c => !c.IsDeleted).Count))
				.ForMember(c => c.BasketCount, map => map.MapFrom(p => p.Baskets.FindAll(c => !c.IsDeleted).Count));
            CreateMap<GetProductImagesDto, ProductImage>().ReverseMap();
			CreateMap<GetProductTagsDto, ProductTag>().ReverseMap();
			CreateMap<GetProductComments, ProductComment>().ReverseMap();

        }
	}
}

