using System;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Interfaces
{
	public interface IProductService : IService<Product>
    {
        Task<int> SaveChanges();
        Task<ResponseObj> UpdateAfterPayment(Product entity);
    }
}

