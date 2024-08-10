using System;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.AdressDto
{
	public class CreateAdressDto
	{
        public string LocationName { get; set; }
        public string Street { get; set; }
        public string CityId { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public CreateAdressDto()
		{
		}
	}
}

