using ECommercePlatform.DTOs;
using FluentValidation;
namespace ECommercePlatform.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MinimumLength(3).WithMessage("Product name must have at least 3 characters")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be less than 0");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("You must select a category for the product");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Product description is too long (max 500 characters)");
        }
    }
}
