using System;
using E_Commerce.Core.Repositories;

namespace E_Commerce.Data
{
	public interface IUnitOfWork : IDisposable
	{
        IChatMessageRepository ChatMessageRepository { get; }
        ICheckRepository CheckRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IContactRepository ContactRepository { get; }
        IProductTagRepository ProductTagRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductCommentRepository ProductCommentRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IBasketRepository BasketRepository { get; }
        IBrandRepository BrandRepository { get; }
        ITagRepository TagRepository { get; }
        IAddressRepository AddressRepository { get; }

        Task<int> Complate();
    }
}

