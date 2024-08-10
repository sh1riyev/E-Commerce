using System;
using AutoMapper;
using E_Commerce.Business.DTOs.BlogDto;
using E_Commerce.Business.DTOs.ProductDto;
using E_Commerce.Business.DTOs.UserDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class BlogProfile:Profile
	{
		public BlogProfile()
		{
			CreateMap<Blog, CreateBlogDto>().ReverseMap();
			CreateMap<Blog, UpdateBlogDto>().ReverseMap();
			CreateMap<Blog, GetBlogByAdminDto>().ReverseMap();
			CreateMap<Blog, GetBlogDto>().ReverseMap();
			CreateMap<GetBlogTagDto, BlogTags>().ReverseMap();
            CreateMap<BlogTags, GetUserBlogTags>().ReverseMap();
            CreateMap<Blog, GetUserBlog>().ReverseMap();
            CreateMap<GetBlogComments, BlogComment>().ReverseMap();

        }
    }
}

