using System;
using FluentValidation;
using E_Commerce.Business.DTOs.ContactDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Validators.ContactValidator
{
	public class CreateContactValidator:AbstractValidator<CreateContactDto>
	{
		public CreateContactValidator()
		{
			RuleFor(c => c.Name)
				.MinimumLength(3).WithMessage("Length of Name must be greater than 3")
				.MaximumLength(100).WithMessage("Length of Name must be smaller than 100");
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("email address is not valid")
                .MinimumLength(3).WithMessage("Length of Email must be greater than 3")
                .MaximumLength(100).WithMessage("Length of Email must be smaller than 100")
                .Matches(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$").WithMessage("email address is not valid"); ;
            RuleFor(c => c.Subject)
                .MinimumLength(3).WithMessage("Length of Subject must be greater than 3")
                .MaximumLength(100).WithMessage("Length of Subject must be smaller than 100");
            RuleFor(c => c.Message)
                .MinimumLength(5).WithMessage("Length of Message must be greater than 5")
                .MaximumLength(300).WithMessage("length of Message must be smaller than 300");

        }
	}
}

