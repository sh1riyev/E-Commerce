using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class City : BaseEntity
	{
        public string Name { get; set; }
        public double DeliverPrice { get; set; }
        [ForeignKey(nameof(Country))]
        public string CountryId { get; set; }
        public Country Country { get; set; }
        public City()
		{
		}
	}
}

