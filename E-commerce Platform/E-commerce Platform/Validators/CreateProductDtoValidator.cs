using ECommercePlatform.Models.Dto;
using FluentValidation;
namespace ECommercePlatform.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa produktu jest wymagana.")
                .MinimumLength(3).WithMessage("Nazwa musi mieć co najmniej 3 znaki.")
                .MaximumLength(100).WithMessage("Nazwa nie może być dłuższa niż 100 znaków.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Cena musi być większa niż 0.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stan magazynowy nie może być ujemny.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Musisz wybrać kategorię dla produktu.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Opis jest za długi (max 500 znaków).");
        }
    }
}
