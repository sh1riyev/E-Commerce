using System;
namespace E_Commerce.Business.DTOs.ContactDto
{
	public class GetContactByAdminDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> RespondedAt { get; set; }
        public bool IsResponded { get; set; }
        public GetContactByAdminDto()
		{
		}
	}
}

