using System;
using FluentValidation;
using E_Commerce.Business.DTOs.AccountDto;

namespace E_Commerce.Business.Validators.AccountValidator
{
	public class UserRegisterValidator:AbstractValidator<UserRegisterDto>
	{
		public UserRegisterValidator()
		{
			RuleFor(u => u.FullName)
				.MinimumLength(5).WithMessage("length of FullName must be greater than 5")
				.MaximumLength(100).WithMessage("length of FullName must be smaller than 100");
			RuleFor(u => u.UserName)
				.MinimumLength(5).WithMessage("length of UserName must be greater than 5")
				.MaximumLength(100).WithMessage("length of UserName must be smaller than 100");
			RuleFor(u => u.Email)
				.EmailAddress().WithMessage(" email address is not valid")
				.MinimumLength(5).WithMessage("email address is not valid")
				.MaximumLength(100).WithMessage("email address is not valid")
				.Matches(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$").WithMessage("email address is not valid");
            RuleFor(u => u.Password)
				.MinimumLength(8).WithMessage("length of password must be greater than 8")
				.MaximumLength(100).WithMessage("length of password must be smaller than 100")
				.Equal(u=>u.ConfirmPassword).WithErrorCode("password and confirmPassowrd must be same");
            RuleFor(u => u.ConfirmPassword)
                .MinimumLength(8).WithMessage("length of password must be greater than 8")
                .MaximumLength(100).WithMessage("length of password must be smaller than 100");
			
        }
	}
}

