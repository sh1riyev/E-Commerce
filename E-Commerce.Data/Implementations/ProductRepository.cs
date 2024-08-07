using System;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;
using E_Commerce.Data.Implimentations;

namespace E_Commerce.Data.Implementations
{
	public class ProductRepository : Repository<Product>,IProductRepository
	{
		public ProductRepository(DataContext context) : base(context) { }
	}
}

