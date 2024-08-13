using System;
using FluentValidation;
using E_Commerce.Business.DTOs.CompaignsDto;

namespace E_Commerce.Business.Validators.CompaignsValidator
{
	public class UpdateCompaignsValidator:AbstractValidator<UpdateCompaignsDto>
	{
		public UpdateCompaignsValidator()
		{
            RuleFor(c => c.Headling)
                .MinimumLength(3).WithMessage("Headling must be greater than 3")
                .MaximumLength(100).WithMessage("Headling must be smaller than 100");
            RuleFor(c => c.Info)
                .MinimumLength(3).WithMessage("Info must be greater than 3")
                .MaximumLength(100).WithMessage("Info must be smaller than 100");
            RuleFor(c => c.Content)
                .MinimumLength(10).WithMessage("Content must be greater than 10")
                .MaximumLength(100).WithMessage("Content must be smaller than 100");
            RuleFor(c => c.Sale).Custom((s, context) =>
            {
                if (s < 0 && s > 100)
                {
                    context.AddFailure("Sale Must be smaller than 100 and greater than 0");
                }
            });
        }
	}
}

