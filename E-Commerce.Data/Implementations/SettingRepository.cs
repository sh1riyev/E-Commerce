using System;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;
using E_Commerce.Data.Implimentations;

namespace E_Commerce.Data.Implementations
{
	public class SettingRepository :Repository<Setting>,ISettingRepository
	{
		public SettingRepository(DataContext context):base(context)
		{
		}
	}
}

