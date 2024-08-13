using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.CompaignsDto
{
	public class GetCompaignsDto
	{
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Headling { get; set; }
        public string Content { get; set; }
        public string Info { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImageUrl { get; set; }
        public double Sale { get; set; }
        public GetCompaignsDto()
		{
		}
	}
}

