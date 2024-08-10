using System;
using   E_Commerce.Core.Entities;

namespace E_Commerce.Business.Interfaces
{
	public interface IWishlistService:IService<Wishlist>
	{
        Task<int> SaveChanges();

    }
}

