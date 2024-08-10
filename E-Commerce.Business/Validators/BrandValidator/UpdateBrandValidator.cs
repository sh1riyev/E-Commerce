using System;
using FluentValidation;
using E_Commerce.Business.DTOs.BrandDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Validators.BrandValidator
{
	public class UpdateBrandValidator:AbstractValidator<UpdateBrandDto>
	{
		public UpdateBrandValidator()
		{
            RuleFor(b => b.Name)
                .MinimumLength(3).WithMessage("length of name must be greater than 3")
                .MaximumLength(100).WithMessage("length of name must be smaller than 100");
        }
	}
}

