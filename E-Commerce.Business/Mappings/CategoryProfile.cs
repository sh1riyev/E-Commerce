using System;
using AutoMapper;
using E_Commerce.Business.DTOs.CategoryDto;
using E_Commerce.Core.Entities;
using E_Commerce.DTOs.CategoryDto;

namespace E_Commerce.Business.Mappings
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, GetCategoryByAdmin>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<GetSubCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryProductDto, Product>().ReverseMap();
            CreateMap<Category, SearchCategoryDto>().ReverseMap();
        }
	}
}

