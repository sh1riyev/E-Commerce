using System;
using FluentValidation;
using E_Commerce.Business.DTOs.ChatMessageDto;

namespace E_Commerce.Business.Validators.ChatMessageValidator
{
	public class UpdateMessageValidator:AbstractValidator<UpdateMessageDto>
	{
		public UpdateMessageValidator()
		{
			RuleFor(m => m.Message).MinimumLength(1).WithMessage("minimun length is 1");
		}
	}
}

