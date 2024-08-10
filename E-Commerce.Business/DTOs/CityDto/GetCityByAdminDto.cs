﻿using System;
namespace E_Commerce.Business.DTOs.CityDto
{
	public class GetCityByAdminDto
	{
        public string Name { get; set; }
        public double DeliverPrice { get; set; }
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string CountryName { get; set; }
        public string CountryId { get; set; }
        public GetCityByAdminDto()
        {
        }
    }
}

