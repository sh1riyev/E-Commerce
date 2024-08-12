using System;
namespace E_Commerce.Business.DTOs.SliderDto
{
	public class GetSliderByAdminDto
	{
        public string Id { get; set; }
        public string AddedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetSliderByAdminDto()
		{
		}
	}
}

