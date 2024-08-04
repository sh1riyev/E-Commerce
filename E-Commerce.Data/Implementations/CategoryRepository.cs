using System;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;

namespace E_Commerce.Data.Implimentations
{
	public class CategoryRepository:Repository<Category>,ICategoryRepository
	{
		public CategoryRepository(DataContext context):base(context)
		{
		}
	}
}

