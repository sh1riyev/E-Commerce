using System;
using FluentValidation;
using E_Commerce.Business.DTOs.BlogDto;

namespace Web_Api.Business.Validators.BlogValidator
{
	public class UpdateBlogValidator:AbstractValidator<UpdateBlogDto>
	{
		public UpdateBlogValidator()
		{
            RuleFor(b => b.Title).MinimumLength(3).WithMessage("Title length must be greater than 3");
            RuleFor(b => b.Content).MinimumLength(5).WithMessage("Content length must be greater than 5");
            RuleFor(b => b.Information).MinimumLength(5).WithMessage("Information length must be greater than 5");
            RuleFor(b => b.Description).MinimumLength(20).WithMessage("Description length must be greater than 20");
        }
	}
}

