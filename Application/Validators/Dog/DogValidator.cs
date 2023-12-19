using Application.Dtos;
using Domain.Models;
using FluentValidation;


namespace Application.Validators.DogValidator
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(dogDto => dogDto.Name)
                .NotEmpty().WithMessage("The name cannot be empty")
                .MaximumLength(50).WithMessage("The name cannot be longer than 50 characters");

            RuleFor(dogDto => dogDto.Breed)
                .NotEmpty().WithMessage("The breed cannot be empty");

            RuleFor(dogDto => dogDto.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");
        }
    }
}