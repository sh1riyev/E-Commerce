﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web_Api.Business.DTO.AccountDto
{
	public class UserUpdateDto
	{
        public IFormFile? ProfileImage { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsSeller { get; set; }
        public string? PhoneNumber { get; set; }
        public UserUpdateDto()
		{
		}
	}
}

