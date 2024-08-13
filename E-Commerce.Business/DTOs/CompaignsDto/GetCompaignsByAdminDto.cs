using System;
namespace E_Commerce.Business.DTOs.CompaignsDto
{
	public class GetCompaignsByAdminDto
	{
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Headling { get; set; }
        public string Content { get; set; }
        public string Info { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public double Sale { get; set; }
        public GetCompaignsByAdminDto()
		{
		}
	}
}

