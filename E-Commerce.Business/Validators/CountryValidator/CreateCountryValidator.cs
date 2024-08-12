using System;
using FluentValidation;
using E_Commerce.Business.DTOs.CountryDto;

namespace E_Commerce.Business.Validators.CountryValidator
{
	public class CreateCountryValidator:AbstractValidator<CreateCountryDto>
	{
		public CreateCountryValidator()
		{
            RuleFor(c => c.Name)
                .MinimumLength(3).WithMessage("Length of Name must be greater than 3")
                .MaximumLength(100).WithMessage("Length of Name must be smaller than 100");
        }

	}
}

