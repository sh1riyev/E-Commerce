using System;
using E_Commerce.Business.DTOs.ProductDto;
using FluentValidation;

namespace E_Commerce.Business.Validators.ProductValidator
{
	public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(100).WithMessage("Lenth of name must be smaller than 100")
                .MinimumLength(3).WithMessage("length of name must be greater than 3");
            RuleFor(p => p.Size)
                .MaximumLength(40).WithMessage("Lenth of size must be smaller than 40")
                .MinimumLength(1).WithMessage("length of name must be greater than 1");
            RuleFor(p => p.Content).MinimumLength(10).WithMessage("length of content must be greater than 10");
            RuleFor(p => p.Color)
                .MinimumLength(3).WithMessage("length of Color must ve greater than 3")
                .MaximumLength(100).WithMessage("length of color must be smaller than 100");
            RuleFor(p => p.Material)
                .MinimumLength(3).WithMessage("length of Material must be greater than 3")
                .MaximumLength(100).WithMessage("length of material must be smaller than 100");
            RuleFor(p => p.Price).Custom((p, context) =>
            {
                if (p < 0)
                {
                    context.AddFailure("price must be greater than 0");
                }
            });
            RuleFor(p => p.Count).Custom((c, context) =>
            {
                if (c < 0)
                {
                    context.AddFailure("count must be greater than 0");
                }
            });
            RuleFor(p => p.Weight).Custom((w, context) =>
            {
                if (w < 0)
                {
                    context.AddFailure("Weight must be greater than 0");
                }
            });
            RuleFor(p => p.SalePercentage).Custom((sp, context) =>
            {
                if (sp < 0 || sp > 100)
                {
                    context.AddFailure("SalePercentage must be greater than 0 and must be smaller than 100");
                }
            });
            RuleFor(p => p.Tax).Custom((t, context) =>
            {
                if (t < 0)
                {
                    context.AddFailure("tax must be greater than 0");
                }
            });
            RuleFor(p => p.TagIds).Custom((i, context) =>
            {
                if (i == null || i.Count == 0)
                {
                    context.AddFailure("Tags is required");
                }
            });
        }
    }
}

