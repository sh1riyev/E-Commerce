using System;
namespace E_Commerce.Business.DTOs.ProductCommentDto
{
	public class UpdateProductCommentDto
	{
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateProductCommentDto()
		{
		}
	}
}

