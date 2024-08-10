using System;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.ProductCommentDto
{
	public class CreateProductCommentDto
	{
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public CreateProductCommentDto()
		{
		}
	}
}

