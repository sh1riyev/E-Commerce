using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Business.DTOs.AccountDto
{
	public class UserRegisterDto
	{
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsSeller { get; set; }
        public UserRegisterDto()
		{
		}
	}
}

