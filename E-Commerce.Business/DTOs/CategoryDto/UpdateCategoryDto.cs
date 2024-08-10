using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.DTOs.CategoryDto
{
	public class UpdateCategoryDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public bool IsMain { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateCategoryDto()
		{
		}
	}
}

