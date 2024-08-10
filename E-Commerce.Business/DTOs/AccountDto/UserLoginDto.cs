using System;
namespace E_Commerce.Business.DTOs.AccountDto
{
	public class UserLoginDto
	{
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
        public UserLoginDto()
		{
		}
	}
}

