using System;
namespace E_Commerce.Business.DTOs.SettingDto
{
	public class GetSettingByAdminDto
	{
        public string Id { get; set; }
        public string AddedBy { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public GetSettingByAdminDto()
		{
		}
	}
}

