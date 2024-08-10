using System;
using FluentValidation;
using E_Commerce.Business.DTOs.TagDto;
using E_Commerce.Core.Entities;

namespace Web_Api.Business.Validators.TagValidator
{
	public class UpdateTagValidator:AbstractValidator<UpdateTagDto>
	{
		public UpdateTagValidator()
		{
            RuleFor(t => t.Name).MinimumLength(3).WithMessage("name of length must be greater than 3");
            RuleFor(t => t.Name).MaximumLength(100).WithMessage("name of length must be smaller than 100");
        }
	}
}

