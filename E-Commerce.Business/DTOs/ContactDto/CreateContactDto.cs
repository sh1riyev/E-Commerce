using System;
namespace E_Commerce.Business.DTOs.ContactDto
{
	public class CreateContactDto
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public CreateContactDto()
		{
		}
	}
}

