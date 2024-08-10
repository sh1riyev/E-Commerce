using System;

namespace E_Commerce.Business.DTOs.BrandDto
{
	public class GetBrandByAdmin
	{
        public string Id { get; set; }
        public string AddedBy { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string PublicId { get; set; }
        public List<GetBrandProductDto> Products { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetBrandByAdmin()
		{
		}
	}
}

