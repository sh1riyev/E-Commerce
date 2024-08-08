using System;
using System.Diagnostics.Contracts;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;

namespace E_Commerce.Data.Implimentations
{
	public class ContactRepository:Repository<Contact>,IContactRepository
    {
		public ContactRepository(DataContext context) : base(context)
        {
		}
	}
}

