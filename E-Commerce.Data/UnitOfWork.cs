using System;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;
using E_Commerce.Data.Implimentations;

namespace E_Commerce.Data
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly DataContext _context;
		public UnitOfWork(DataContext context)
		{
            _context = context;
            ChatMessageRepository = new ChatMessageRepository(_context);
        }

        public IChatMessageRepository ChatMessageRepository { get; private set; }


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

