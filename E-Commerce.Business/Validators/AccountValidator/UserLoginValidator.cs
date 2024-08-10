using System;
using FluentValidation;
using E_Commerce.Business.DTOs.AccountDto;

namespace E_Commerce.Business.Validators.AccountValidator
{
	public class UserLoginValidator:AbstractValidator<UserLoginDto>
	{
		public UserLoginValidator()
		{
			RuleFor(u => u.EmailOrUserName)
				.MaximumLength(100).WithMessage("length of EmailOrUserName must be smaller than 1oo")
				.MinimumLength(5).WithMessage("length of EmailOrUserName must be greater than 5");
			RuleFor(u => u.Password)
				.MaximumLength(30).WithMessage("length of paswword must be smaller than 30")
				.MinimumLength(8).WithMessage("length of password must be greater than 8");

		}
	}
}

