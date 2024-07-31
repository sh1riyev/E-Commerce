using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.DTOs.CategoryDto
{
	public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public bool IsMain { get; set; }
        public IFormFile Image { get; set; }
        public CategoryCreateDto()
        {
        }
    }
}

