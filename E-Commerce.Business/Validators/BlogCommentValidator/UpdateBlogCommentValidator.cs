using System;
using FluentValidation;
using	E_Commerce.Business.DTOs.BlogCommentDto;

namespace E_Commerce.Business.Validators.BlogCommentValidator
{
	public class UpdateBlogCommentValidator:AbstractValidator<UpdateBlogCommentDto>
	{
		public UpdateBlogCommentValidator()
		{
            RuleFor(pc => pc.Content).MinimumLength(1).WithMessage("content must not be null");
            
        }
	}
}

