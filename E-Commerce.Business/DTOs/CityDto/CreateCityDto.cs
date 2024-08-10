using System;
namespace E_Commerce.Business.DTOs.CityDto
{
	public class CreateCityDto
	{
        public string Name { get; set; }
        public double DeliverPrice { get; set; }
        public string CountryId { get; set; }
        public CreateCityDto()
        {
        }
    }
}

