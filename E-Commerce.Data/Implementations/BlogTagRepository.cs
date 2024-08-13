using System;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;

namespace E_Commerce.Data.Implimentations
{
	public class BlogTagRepository:Repository<BlogTags>,IBlogTagRepository
	{
		public BlogTagRepository(DataContext context) : base(context)
        {
		}
	}
}

