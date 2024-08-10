using System;
using FluentValidation;
using E_Commerce.Business.DTOs.CheckDto;

namespace EventArgs.Business.Validators.CheckValidator
{
	public class UpdateCheckValidator:AbstractValidator<UpdateCheckDto>
	{
		public UpdateCheckValidator()
		{
			RuleFor(c => c.Status).Custom((s, ctx) =>
			{
				if (s<=0||s>20)
				{
					ctx.AddFailure("Status must be smaller than 20 and gretaer than 0");
				}
			});
        }
	}
}

