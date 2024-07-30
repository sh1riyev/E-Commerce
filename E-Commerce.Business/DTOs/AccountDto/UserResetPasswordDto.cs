using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Api.Business.DTO.AccountDto
{
	public class UserResetPasswordDto
	{
		public string Email { get; set; }
		public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserResetPasswordDto()
		{
		}
	}
}

