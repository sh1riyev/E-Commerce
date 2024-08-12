using System;
using FluentValidation;
using E_Commerce.Business.DTOs.ProductCommentDto;

namespace E_Commerce.Business.Validators.ProductCommentValidator
{
	public class CreateProductCommentValidator:AbstractValidator<CreateProductCommentDto>
	{
		public CreateProductCommentValidator()
		{
			RuleFor(pc => pc.Content).MinimumLength(1).WithMessage("content must not be null");
			RuleFor(pc => pc.Rating).Custom((r, context) =>
			{
				if (r<0||r>5)
				{
					context.AddFailure("rating must be [0,5] interval");
				}
			});
		}
	}
}

