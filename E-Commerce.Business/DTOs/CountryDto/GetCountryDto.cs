using System;
namespace E_Commerce.Business.DTOs.CountryDto
{
	public class GetCountryDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public GetCountryDto()
		{
		}
	}
}

