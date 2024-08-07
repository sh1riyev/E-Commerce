using System;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;

namespace E_Commerce.Data.Implimentations
{
	public class BrandRepository:Repository<Brand>,IBrandRepository
	{
		public BrandRepository(DataContext context) : base(context)
        {
		}
	}
}

