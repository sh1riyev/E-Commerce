using System;
using Web_Api.Core.Entities;
using Web_Api.Core.Repositories;

namespace Web_Api.Data.Implimentations
{
	public class ProductImageRepository:Repository<ProductImage>,IProductImageRepository
	{
		public ProductImageRepository(DataContext context) : base(context)
        {
		}
	}
}

