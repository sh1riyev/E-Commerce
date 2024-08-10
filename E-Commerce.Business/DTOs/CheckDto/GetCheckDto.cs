using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Business.DTOs.CheckDto
{
	public class GetCheckDto
	{

        public string Id { get; set; }
        public double TotalAmmount { get; set; }
        public double Sale { get; set; }
        public string AdressId { get; set; }
        public string AdressLocationName { get; set; }
        public string AdressStreet { get; set; }
        public string AdressState { get; set; }
        public string AdressZipCode { get; set; }
        public string AdressCityName { get; set; }
        public string AdressCityDeliverPrice { get; set; }
        public string AdressCityCountryName { get; set; }
        public List<GetCheckProductDto> CheckProducts { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public string AppUserUserName { get; set; }
        public string AppUserPhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeliverAt { get; set; }
        public string? Promocode { get; set; }
        public bool IsDeleted { get; set; }
        public GetCheckDto()
		{
		}
	}
}

