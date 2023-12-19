using Domain.Models.Animal;
using FluentValidation;

namespace Application.Validators
{
    public class AnimalModelValidator : AbstractValidator<AnimalModel>
    {
        public AnimalModelValidator()
        {
            RuleFor(animal => animal.Name)
                .NotEmpty().WithMessage("Namnet får inte vara tomt")
                .MaximumLength(50).WithMessage("Namnet får inte vara längre än 50 tecken");
        }
    }
}