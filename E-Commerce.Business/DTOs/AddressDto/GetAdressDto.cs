using System;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.AdressDto
{
	public class GetAdressDto
	{
        public string Id { get; set; }
        public string LocationName { get; set; }
        public string Street { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string CityDeliverPrice { get; set; }
        public string CityCountryName { get; set; }
        public string CityCountryId { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string AppUserFullName { get; set; }
        public string AppUserPhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public GetAdressDto()
		{
		}
	}
}

