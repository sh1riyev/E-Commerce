using System;
namespace E_Commerce.Business.DTOs.SubscribeDto
{
	public class GetSubscribeByAdminDto
	{
        public string Id { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string AddedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetSubscribeByAdminDto()
		{
		}
	}
}

