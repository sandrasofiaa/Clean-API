using Application.Dtos;
using Domain.Models;
using FluentValidation;

namespace Application.Validators.CatValidator
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(cat => cat.LikesToPlay)
                .NotNull().WithMessage("Please specify if the cat likes to play.");

            RuleFor(cat => cat.Breed)
                .NotEmpty().WithMessage("Please provide the cat's breed.")
                .MaximumLength(50).WithMessage("Breed should not exceed 50 characters.");

            RuleFor(cat => cat.Weight)
                .GreaterThan(0).WithMessage("Weight should be greater than zero.");
        }
    }
}
