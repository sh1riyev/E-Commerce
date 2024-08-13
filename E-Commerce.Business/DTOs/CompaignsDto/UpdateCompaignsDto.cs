using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.CompaignsDto
{
	public class UpdateCompaignsDto
	{
        public string Id { get; set; }
        public string Headling { get; set; }
        public string Content { get; set; }
        public string Info { get; set; }
        public DateTime ExpirationDate { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsDeleted { get; set; }
        public double Sale { get; set; }
        public UpdateCompaignsDto()
		{
		}
	}
}

