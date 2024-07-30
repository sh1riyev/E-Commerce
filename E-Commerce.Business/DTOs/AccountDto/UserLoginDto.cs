using System;
namespace Web_Api.Business.DTO.AccountDto
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

