using System;
using E_Commerce.Data.Data;

namespace E_Commerce.Data
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly DataContext _context;
		public UnitOfWork(DataContext context)
		{
            _context = context;
		}

        public async Task<int> Complate()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

