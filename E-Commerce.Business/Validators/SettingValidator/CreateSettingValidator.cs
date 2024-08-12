using System;
using FluentValidation;
using E_Commerce.Business.DTOs.SettingDto;

namespace E_Commerce.Business.Validators.SettingValidator
{
	public class CreateSettingValidator:AbstractValidator<CreateSettingDto>
	{
		public CreateSettingValidator()
		{
			RuleFor(s => s.Key)
				.MinimumLength(3).WithMessage("length of Key must be greater than 3")
				.MaximumLength(100).WithMessage("length of Key must be smaller than 100");
            RuleFor(s => s.Value)
                .MinimumLength(3).WithMessage("length of Value must be greater than 3")
                .MaximumLength(300).WithMessage("length of Value must be smaller than 300");
        }
	}
}

