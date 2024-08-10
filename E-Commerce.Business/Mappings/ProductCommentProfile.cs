using System;
using AutoMapper;
using E_Commerce.Business.DTOs.ProductCommentDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings.AuthoMapper
{
	public class ProductCommentProfile:Profile
	{
		public ProductCommentProfile()
		{
			CreateMap<ProductComment, CreateProductCommentDto>().ReverseMap();
			CreateMap<ProductComment, UpdateProductCommentDto>().ReverseMap();
			CreateMap<ProductComment, GetProductCommentByAdminDto>().ReverseMap();
			CreateMap<ProductComment, GetProductCommentDto>().ReverseMap();
        }
	}
}

