using System;
using FluentValidation;
using E_Commerce.Business.DTOs.FilterDto;

namespace E_Commerce.Business.Validators.FilterValidator
{
	public class FilterStatusValidator:AbstractValidator<FilterStatus>
	{
		public FilterStatusValidator()
		{
			RuleFor(s => s.Status).Custom((status, ctx) =>
			{
				if (status<1||status>9)
				{
					ctx.AddFailure("incorrect filter status");
				}
			});
		}
	}
}

