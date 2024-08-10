using System;
namespace E_Commerce.Business.DTOs.ProductCommentDto
{
	public class GetProductCommentByAdminDto
	{
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserId { get; set; }
        public string AppUserFullName { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public GetProductCommentByAdminDto()
		{
		}
	}
}

