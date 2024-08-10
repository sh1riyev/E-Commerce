using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace E_Commerce.Business.DTOs.ProductDto
{
	public class GetProductComments
	{
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string AppUserFullName { get; set; }
        public string AppUserProfileImageUrl { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetProductComments()
		{
		}
	}
}

