using System;
using FluentValidation;
using E_Commerce.Business.DTOs.BlogCommentDto;

namespace E_Commerce.Business.Validators.BlogCommentValidator
{
	public class CreateBlogCommentValidator:AbstractValidator<CreateBlogCommentDto>
	{
		public CreateBlogCommentValidator()
		{
            RuleFor(c => c.Content).MinimumLength(1).WithMessage("content must not be null");
           
        }
	}
}

