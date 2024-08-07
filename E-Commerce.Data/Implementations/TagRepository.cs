using System;
using E_Commerce.Core.Entities;
using E_Commerce.Data.Data;
using Web_Api.Core.Repositories;

namespace E_Commerce.Data.Implimentations
{
	public class TagRepository:Repository<Tag>,ITagRepository
	{
		public TagRepository(DataContext context) : base(context)
        {
		}
	}
}

