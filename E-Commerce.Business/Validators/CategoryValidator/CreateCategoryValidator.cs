using System;
using FluentValidation;
using E_Commerce.DTOs.CategoryDto;

namespace E_Commerce.Validators.CategoryValidator
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Lenth of name must be smaller than 100")
                .MinimumLength(3).WithMessage("length of name must be greater than 3");
        
        }
    }
}

