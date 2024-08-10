using System;
using System.Linq.Expressions;
using E_Commerce.Business.DTOs.ResponseDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Interfaces
{
	public interface IService<T>where T:BaseEntity
	{
		Task<ResponseObj> Create(T entity);
		Task<ResponseObj> Delete(string id);
		Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes);
		Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
		Task<bool> IsExist(Expression<Func<T, bool>> predicate = null);
		Task<ResponseObj> Update(T entity);
	}
}

