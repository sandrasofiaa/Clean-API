using Application.Queries.Cats.GetCatsByBreedAndWeight;
using FluentValidation;

namespace Application.Queries.Cats.GetCatBreedAndWeight
{
    public class GetCatsByBreedAndWeightQueryValidator : AbstractValidator<GetCatsByBreedAndWeightQuery>
    {
        public GetCatsByBreedAndWeightQueryValidator()
        {

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("The weight must be greater than 0.");
        }
    }
}