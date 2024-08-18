using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Entities
{
	public class Check : BaseEntity
	{
        public double TotalAmmount { get; set; }
        public double Sale { get; set; }
        [ForeignKey(nameof(Adress))]
        public string AdressId { get; set; }
        public Adress Adress { get; set; }
        public List<CheckProduct> CheckProducts { get; set; }
        public int Status { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string? Promocode { get; set; }
        public Nullable<DateTime> DeliverAt { get; set; }
        public Check()
        {
            CheckProducts = new();
        }
    }
}

