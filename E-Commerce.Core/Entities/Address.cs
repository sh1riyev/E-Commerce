using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class Address : BaseEntity
	{
        public string LocationName { get; set; }
        public string Street { get; set; }
        [ForeignKey(nameof(City))]
        public string CityId { get; set; }
        public City City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Address()
		{
		}
	}
}

