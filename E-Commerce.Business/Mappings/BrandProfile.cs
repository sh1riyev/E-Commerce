using System;
using AutoMapper;
using	E_Commerce.Business.DTOs.BrandDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class BrandProfile:Profile
	{
		public BrandProfile()
		{
			CreateMap<CreateBrandDto, Brand>();
			CreateMap<UpdateBrandDto, Brand>().ReverseMap();
			CreateMap<Brand, GetBrandByAdmin > ().ReverseMap();
			CreateMap<Brand, GetBrandDto > ().ReverseMap();
			CreateMap<GetBrandProductDto, Product>().ReverseMap();
			CreateMap<Brand, SearchBrandDto>().ReverseMap();
        }
	}
}

