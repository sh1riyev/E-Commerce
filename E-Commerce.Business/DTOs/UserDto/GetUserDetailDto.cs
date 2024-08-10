

namespace E_Commerce.Business.DTOs.UserDto
{
	public class GetUserDetailDto
	{
        public string ProfileImageUrl { get; set; }
        public string PublicId { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> RemovedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsSeller { get; set; }
        public int ProductsCount { get; set; }
        public int BlogsCount { get; set; }
        public int  WishlistsCount { get; set; }
        public GetUserDetailDto()
		{
		}
	}
}

