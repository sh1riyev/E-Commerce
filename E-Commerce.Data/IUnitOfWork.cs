using System;
using E_Commerce.Core.Repositories;
using E_Commerce.Data.Implementations;
using E_Commerce.Data.Implimentations;

namespace E_Commerce.Data
{
	public interface IUnitOfWork : IDisposable
	{
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISettingRepository SettingRepository { get; }
        ISliderRepository SliderRepository { get; }
        ISubscribeRepository SubscribeRepository { get; }
        ITagRepository TagRepository { get; }
        IContactRepository ContactRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductTagRepository ProductTagRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductCommentRepository ProductCommentRepository { get; }
        IBlogRepository BlogRepository { get; }
        IBlogTagRepository BlogTagRepository { get; }
        IBlogCommentRepository BlogCommentRepository { get; }
        ICampaignsRepository CampaignsRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IBasketRepository BasketRepository { get; }
        IAddressRepository AddressRepository { get; }
        ICheckProductRepository CheckProductRepository { get; }
        ICheckRepository CheckRepository { get; }
        ICityRepository CityRepository { get; }
        ICountryRepository CountryRepository { get; }
        IChatMessageRepository ChatMessageRepository { get; }
        Task<int> Complate();
    }
}

