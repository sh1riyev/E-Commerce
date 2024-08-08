using System;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;

namespace E_Commerce.Data.Implimentations
{
	public class ProductCommentRepository:Repository<ProductComment>,IProductCommentRepository
	{
		public ProductCommentRepository(DataContext context) : base(context)
        {
		}
	}
}

