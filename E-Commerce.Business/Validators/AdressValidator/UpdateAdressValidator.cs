using System;
using FluentValidation;
using E_Commerce.Business.DTOs.AdressDto;

namespace E_Commerce.Business.Validators.AdressValidator
{
	public class UpdateAdressValidator: AbstractValidator<UpdateAdressDto>
    {

		public UpdateAdressValidator()
		{
            RuleFor(a => a.LocationName)
                .MinimumLength(3).WithMessage("length of CompanyName must be greater than 3")
                .MaximumLength(100).WithMessage("length of CompanyName must be smaller than 100");
            RuleFor(a => a.State)
                .MinimumLength(3).WithMessage("length of State must be greater than 3")
                .MaximumLength(100).WithMessage("length of State must be smaller than 100");
            RuleFor(a => a.Street)
                .MinimumLength(3).WithMessage("length of Street must be greater than 3")
                .MaximumLength(100).WithMessage("length of Street must be smaller than 100");
            RuleFor(a => a.ZipCode)
                .MinimumLength(3).WithMessage("length of ZipCode must be greater than 3")
                .MaximumLength(100).WithMessage("length of ZipCode must be smaller than 100");
        }
	}
}

