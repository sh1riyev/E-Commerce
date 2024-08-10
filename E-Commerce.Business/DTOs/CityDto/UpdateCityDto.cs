using System;
namespace E_Commerce.Business.DTOs.CityDto
{
	public class UpdateCityDto
	{
        public string Name { get; set; }
        public double DeliverPrice { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateCityDto()
        {
        }
    }
}

