using System;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Data;
using E_Commerce.Data.Implementations;
using E_Commerce.Data.Implimentations;

namespace E_Commerce.Data
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly DataContext _context;
		public UnitOfWork(DataContext context)
		{
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ChatMessageRepository = new ChatMessageRepository(_context);
            CheckRepository = new CheckRepository(_context);
            ProductRepository = new ProductRepository(_context);
            ContactRepository = new ContactRepository(_context);
            ProductTagRepository = new ProductTagRepository(_context);
            ProductImageRepository = new ProductImageRepository(_context);
            BrandRepository = new BrandRepository(_context);
            TagRepository = new TagRepository(_context);
            ProductCommentRepository = new ProductCommentRepository(_context);
            WishlistRepository = new WishlistRepository(_context);
            BasketRepository = new BasketRepository(_context);
        }

        public ICategoryRepository CategoryRepository { get; private set; }
        public IChatMessageRepository ChatMessageRepository { get; private set; }
        public ICheckRepository CheckRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IContactRepository ContactRepository { get; private set; }
        public IProductTagRepository ProductTagRepository { get; private set; }
        public IProductImageRepository ProductImageRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public IProductCommentRepository ProductCommentRepository { get; private set; }
        public IWishlistRepository WishlistRepository { get; private set; }
        public IBasketRepository BasketRepository { get; private set; }

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

