using System;
using Web_Api.Core.Entities;
using Web_Api.Core.Repositories;

namespace Web_Api.Data.Implimentations
{
	public class ProductTagRepository:Repository<ProductTag>,IProductTagRepository
	{
		public ProductTagRepository(DataContext context) : base(context)
        {
		}
	}
}

