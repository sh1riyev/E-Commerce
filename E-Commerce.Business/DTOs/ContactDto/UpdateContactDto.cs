using System;
namespace E_Commerce.Business.DTOs.ContactDto
{
	public class UpdateContactDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsResponded { get; set; }
        public UpdateContactDto()
		{
		}
	}
}

