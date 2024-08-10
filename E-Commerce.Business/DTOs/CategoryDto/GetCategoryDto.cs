using System;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Business.DTOs.CategoryDto;

namespace E_Commerce.DTOs.CategoryDto
{
	public class GetCategoryDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public bool IsMain { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetSubCategoryDto> SubCategories { get; set; }
        public List<GetCategoryProductDto> Products { get; set; }
        public GetCategoryDto()
        {
            SubCategories = new();
            Products = new();
        }
        
	}
}

