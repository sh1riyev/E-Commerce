using System;
namespace E_Commerce.Business.DTOs.CountryDto
{
	public class UpdateCountryDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public UpdateCountryDto()
		{
		}
	}
}

