using System;

namespace E_Commerce.Business.DTOs.CategoryDto
{
	public class GetCategoryByAdmin
	{
        public string Id { get; set; }
        public string AddedBy { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public bool IsMain { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetCategoryByAdmin()
        {
        }
    }
}

