using System;
using FluentValidation;
using E_Commerce.Business.DTOs.TagDto;

namespace E_Commerce.Business.Validators.TagValidator
{
	public class CreateTagValidator:AbstractValidator<CreateTagDto>
	{
		public CreateTagValidator()
		{
			RuleFor(t => t.Name).MinimumLength(3).WithMessage("name of length must be greater than 3");
			RuleFor(t => t.Name).MaximumLength(100).WithMessage("name of length must be smaller than 100");
        }
	}
}

