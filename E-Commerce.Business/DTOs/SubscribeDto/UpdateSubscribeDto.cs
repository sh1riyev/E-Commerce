using System;
namespace E_Commerce.Business.DTOs.SubscribeDto
{
	public class UpdateSubscribeDto
	{
        public string Id { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateSubscribeDto()
		{
		}
	}
}

