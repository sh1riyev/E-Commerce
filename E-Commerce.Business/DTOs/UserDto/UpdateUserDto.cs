using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Business.DTOs.UserDto
{
	public class UpdateUserDto
	{
        public IList<string> Roles { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSeller { get; set; }
        public UpdateUserDto()
		{
		}
	}
}

