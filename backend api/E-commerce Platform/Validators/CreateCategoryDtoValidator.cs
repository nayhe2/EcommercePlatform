using ECommercePlatform.DTOs;
using FluentValidation;

namespace ECommercePlatform.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MinimumLength(3).WithMessage("Category name must have at least 3 characters")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");
        }
    }
}
