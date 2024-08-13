using System;
using AutoMapper;
using E_Commerce.Business.DTOs.BlogCommentDto;
using E_Commerce.Business.DTOs.ProductCommentDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class BlogCommentProfile:Profile
	{
		public BlogCommentProfile()
		{
            CreateMap<BlogComment, CreateBlogCommentDto>().ReverseMap();
            CreateMap<BlogComment, UpdateBlogCommentDto>().ReverseMap();
            CreateMap<BlogComment, GetBlogCommentByAdminDto>().ReverseMap();
            CreateMap<BlogComment, GetBlogCommentDto>().ReverseMap();
        }
	}
}

