using Domain.Models;
using FluentValidation;

namespace Application.Validators.BirdValidator
{
    public class BirdValidator : AbstractValidator<Bird>
    {
        public BirdValidator()
        {
            RuleFor(bird => bird.CanFly)
                .NotNull().WithMessage("Please specify if the bird can fly.");

            RuleFor(bird => bird.Color)
                .NotEmpty().WithMessage("Please provide the bird's color.")
                .MaximumLength(50).WithMessage("Color should not exceed 50 characters.");
        }
    }
}