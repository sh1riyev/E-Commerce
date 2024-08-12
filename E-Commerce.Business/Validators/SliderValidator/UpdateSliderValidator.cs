using System;
using FluentValidation;
using E_Commerce.Business.DTOs.SliderDto;

namespace E_Commerce.Business.Validators.SliderValidator
{
	public class UpdateSliderValidator: AbstractValidator<UpdateSliderDto>
    {
		public UpdateSliderValidator()
		{
            RuleFor(s => s.Title)
                .MinimumLength(3).WithMessage("minumun length of Title must be greater than 3")
                .MaximumLength(100).WithMessage("maximun length of Title must be smaller than 100");
            RuleFor(s => s.Information)
                .MinimumLength(3).WithMessage("minumun length of Information must be greater than 3")
                .MaximumLength(100).WithMessage("maximun length of Information must be smaller than 100");
            RuleFor(s => s.Description)
                .MinimumLength(10).WithMessage("minumun length of Description must be greater than 10");
            RuleFor(s => s.Content)
                .MinimumLength(10).WithMessage("minumun length of Content must be greater than 10");
        }
	}
}

