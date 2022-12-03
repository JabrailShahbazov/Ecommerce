using Ecommerce.Application.ViewModels.Product;
using FluentValidation;

namespace Ecommerce.Application.Validators.Products;

public class CreateProductValidator : AbstractValidator<CreateProductVM>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is not empty")
            .MaximumLength(150)
            .MinimumLength(2)
            .WithMessage("character between 150 or 2");
        
        RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is not empty")
            .Must(s => s >= 0)
            .WithMessage("Stock is Not Negative");
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is not empty")
            .Must(s => s >= 0)
            .WithMessage("Price is Not Negative");
    }
}