using System;

namespace E_Commerce.Business.DTOs.CategoryDto
{
	public class GetSubCategoryDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public GetSubCategoryDto()
		{
		}
	}
}

