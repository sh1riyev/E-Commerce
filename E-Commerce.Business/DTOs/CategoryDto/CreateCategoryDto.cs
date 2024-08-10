using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.DTOs.CategoryDto
{
	public class CreateCategoryDto
	{
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public bool IsMain { get; set; }
        public IFormFile Image { get; set; }
        public CreateCategoryDto()
		{
		}
	}
}

