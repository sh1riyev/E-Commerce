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
            BrandRepository = new BrandRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            SettingRepository = new SettingRepository(_context);
            SliderRepository = new SliderRepository(_context);
            SubscribeRepository = new SubscribeRepository(_context);
            TagRepository = new TagRepository(_context);
            ContactRepository = new ContactRepository(_context);
            ProductRepository = new ProductRepository(_context);
            ProductTagRepository = new ProductTagRepository(_context);
            ProductImageRepository = new ProductImageRepository(_context);
            ProductCommentRepository = new ProductCommentRepository(_context);
            BlogRepository = new BlogRepository(_context);
            BlogTagRepository = new BlogTagRepository(_context);
            BlogCommentRepository = new BlogCommentRepository(_context);
            CompaignsRepository = new CompaignsRepository(_context);
            WishlistRepository = new WishlistRepository(_context);
            BasketRepository = new BasketRepository(_context);
            AdressRepository = new AdressRepository(_context);
            CheckRepository = new CheckRepository(_context);
            CheckProductRepository = new CheckProductRepository(_context);
            CityRepository = new CityRepository(_context);
            CountryRepository = new CountryRepository(_context);
            ChatMessageRepository = new ChatMessageRepository(_context);
        }

        public IBrandRepository BrandRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public ISettingRepository SettingRepository { get; private set; }

        public ISliderRepository SliderRepository { get; private set; }

        public ISubscribeRepository SubscribeRepository { get; private set; }

        public ITagRepository TagRepository { get; private set; }

        public IContactRepository ContactRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IProductTagRepository ProductTagRepository { get; private set; }

        public IProductImageRepository ProductImageRepository { get; private set; }

        public IProductCommentRepository ProductCommentRepository { get; private set; }

        public IBlogTagRepository BlogTagRepository { get; private set; }

        public IBlogRepository BlogRepository { get; private set; }

        public IBlogCommentRepository BlogCommentRepository { get; private set; }

        public ICompaignsRepository CompaignsRepository { get; private set; }

        public IWishlistRepository WishlistRepository { get; private set; }

        public IBasketRepository BasketRepository { get; private set; }

        public IAdressRepository AdressRepository { get; private set; }

        public ICheckRepository CheckRepository { get; private set; }

        public ICheckProductRepository CheckProductRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public ICountryRepository CountryRepository { get; private set; }

        public IChatMessageRepository ChatMessageRepository { get; private set; }

        public async Task<int> Complate()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

