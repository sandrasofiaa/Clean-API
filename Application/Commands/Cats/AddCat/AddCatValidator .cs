using Application.Dtos;
using FluentValidation;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatValidator : AbstractValidator<CatDto>
    {
        public AddCatValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(dto => dto.Breed)
                .MaximumLength(50).WithMessage("Breed cannot exceed 50 characters.");

            RuleFor(dto => dto.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than zero.");
        }
    }
}