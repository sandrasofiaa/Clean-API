using Application.Dtos;
using FluentValidation;

namespace Application.Commands.Dogs.AddDog
{
    public class AddDogValidator : AbstractValidator<DogDto>
    {
        public AddDogValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(dto => dto.Breed)
                .MaximumLength(50).WithMessage("Breed cannot exceed 50 characters.");

            RuleFor(dto => dto.Weight)
                .GreaterThan(0).When(dto => dto.Weight.HasValue)
                .WithMessage("Weight must be greater than zero.");
        }
    }
}