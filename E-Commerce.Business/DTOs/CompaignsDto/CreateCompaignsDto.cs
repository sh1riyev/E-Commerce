using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.CompaignsDto
{
	public class CreateCompaignsDto
	{
        public string Headling { get; set; }
        public string Content { get; set; }
        public string Info { get; set; }
        public DateTime ExpirationDate { get; set; }
        public IFormFile Image { get; set; }
        public double Sale { get; set; }
        public CreateCompaignsDto()
		{
		}
	}
}

