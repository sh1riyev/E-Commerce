using System;
using FluentValidation;
using E_Commerce.DTOs.CategoryDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Validators.CategoryValidator
{
	public class UpdateCategoryValidator:AbstractValidator<UpdateCategoryDto>
	{
		public UpdateCategoryValidator()
		{
			RuleFor(c => c.Name)
				.NotNull().WithMessage("Name must be required")
                .MaximumLength(100).WithMessage("Lenth of name must be smaller than 100")
                .MinimumLength(3).WithMessage("Name length must be greater than 3");

           

        }
	}
}

