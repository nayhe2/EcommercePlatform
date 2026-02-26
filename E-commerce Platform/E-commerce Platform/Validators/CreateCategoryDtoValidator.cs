using ECommercePlatform.DTOs;
using FluentValidation;

namespace ECommercePlatform.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa kategorii jest wymagana.")
                .MinimumLength(3).WithMessage("Nazwa kategorii musi mieć co najmniej 3 znaki.")
                .MaximumLength(50).WithMessage("Nazwa kategorii nie może być dłuższa niż 50 znaków.");
        }
    }
}
