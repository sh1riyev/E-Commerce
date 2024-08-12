using System;
using FluentValidation;
using E_Commerce.Business.DTOs.CityDto;

namespace E_Commerce.Business.Validators.CityValidator
{
	public class CreateCityValidator:AbstractValidator<CreateCityDto>
	{
		public CreateCityValidator()
		{
            RuleFor(p => p.Name)
                .MinimumLength(3).WithMessage("length of Name must be greater than 3")
                .MaximumLength(100).WithMessage("length of Name must be smaller than 100");
            RuleFor(p => p.DeliverPrice).Custom((p, context) =>
            {
                if (p < 0)
                {
                    context.AddFailure("DeliverPrice must be greater than 0");
                }
            });
        }
	}
}

