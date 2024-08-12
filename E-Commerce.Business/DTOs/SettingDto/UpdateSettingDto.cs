using System;
namespace E_Commerce.Business.DTOs.SettingDto
{
	public class UpdateSettingDto
	{
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateSettingDto()
		{
		}
	}
}

