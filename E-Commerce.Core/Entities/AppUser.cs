using System;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Core.Entities
{
	public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PublicId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> RemovedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsSeller { get; set; }
        public string AddedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool isOnline { get; set; }
        public string ? ConnectionId { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<BlogComment> BlogComments { get; set; }
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
        public List<Check> Checkes { get; set; }
        public List<Address> Adresses { get; set; }
        public List<Wishlist> Wishlists { get; set; }
        public List<Basket> Baskets { get; set; }
        public AppUser()
		{
		}
	}
}

