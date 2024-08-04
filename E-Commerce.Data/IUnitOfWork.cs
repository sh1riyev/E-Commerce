using System;
using E_Commerce.Core.Repositories;

namespace E_Commerce.Data
{
	public interface IUnitOfWork : IDisposable
	{
        IChatMessageRepository ChatMessageRepository { get; }
        ICheckRepository CheckRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> Complate();
    }
}

