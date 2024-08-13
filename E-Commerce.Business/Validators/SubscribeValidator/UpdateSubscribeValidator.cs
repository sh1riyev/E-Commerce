using System;
using FluentValidation;
using E_Commerce.Business.DTOs.SubscribeDto;

namespace E_Commerce.Business.Validators.SubscribeValidator
{
	public class UpdateSubscribeValidator : AbstractValidator<UpdateSubscribeDto>
    {
		public UpdateSubscribeValidator()
		{
            RuleFor(s => s.Email)
                .EmailAddress().WithMessage(" email address is not valid")
                .Matches(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$").WithMessage("email address is not valid")
                .MinimumLength(3).WithMessage("minimun length of email must be greate than 3")
                .MaximumLength(100).WithMessage("maximun length of email must be smaller than 100");

            RuleFor(s => s.Gender)
                .MinimumLength(3).WithMessage("minimun length of Gender must be greate than 3")
                .MaximumLength(100).WithMessage("maximun length of Gender must be smaller than 100")
                .Custom((s, context) =>
                {
                    if (s.ToLower() != "female" && s.ToLower() != "male")
                    {
                        context.AddFailure("gender must be only Male or Female");
                    }
                });
        }
	}
}

