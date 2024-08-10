using System;
using   E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Interfaces
{
	public interface IBasketService:IService<Basket>
	{
         Task<ResponseObj> IncreaseCount(Basket entity);
         Task<ResponseObj> DecreaseCount(Basket entity);
        Task<int> SaveChanges();
    }
}

