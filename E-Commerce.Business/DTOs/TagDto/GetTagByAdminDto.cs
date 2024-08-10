using System;
namespace E_Commerce.Business.DTOs.TagDto
{
	public class GetTagByAdminDto
	{
        public string Id { get; set; }
        public string AddedBy { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetTagByAdminDto()
		{
		}
	}
}

