using System;
namespace E_Commerce.Business.DTOs.CountryDto
{
	public class GetCountryByAdminDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public GetCountryByAdminDto()
		{
		}
	}
}

