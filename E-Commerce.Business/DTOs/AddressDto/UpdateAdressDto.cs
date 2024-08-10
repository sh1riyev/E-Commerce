using System;
namespace E_Commerce.Business.DTOs.AdressDto
{
	public class UpdateAdressDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string LocationName { get; set; }
        public string Street { get; set; }
        public string CityId { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public UpdateAdressDto()
		{
		}
	}
}

