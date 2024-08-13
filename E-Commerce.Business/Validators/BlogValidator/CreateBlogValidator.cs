using System;
using FluentValidation;
using E_Commerce.Business.DTOs.BlogDto;

namespace E_Commerce.Business.Validators.BlogValidator
{
	public class CreateBlogValidator:AbstractValidator<CreateBlogDto>
	{

		public CreateBlogValidator()
		{
			RuleFor(b => b.Title)
				.MinimumLength(3).WithMessage("Title length must be greater than 3");
			RuleFor(b => b.Content).MinimumLength(5).WithMessage("Content length must be greater than 5");
			RuleFor(b => b.Information).MinimumLength(5).WithMessage("Information length must be greater than 5");
			RuleFor(b => b.Description).MinimumLength(20).WithMessage("Description length must be greater than 20");
            RuleFor(b => b.TagIds).Custom((i, context) =>
            {
                if (i == null || i.Count == 0)
                {
                    context.AddFailure("Tags is required");
                }
            });
        }
	}
}

