using System;
namespace E_Commerce.Business.DTOs.ContactDto
{
	public class GetContactDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public GetContactDto()
		{
		}
	}
}

