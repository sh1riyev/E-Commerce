using System;
using FluentValidation;
using E_Commerce.Business.DTOs.BasketDto;

namespace E_Commerce.Business.Validators.BasketValidator
{
	public class UpdateBasketValidator:AbstractValidator<UpdateBasketDto>
	{
		public UpdateBasketValidator()
		{
			RuleFor(b => b.Count).Custom((c, ctx) =>
			{
				if (c <= 0)
				{
					ctx.AddFailure("count must be greater than 0");
				}
			});
		}
	}
}

