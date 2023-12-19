using FluentValidation;

namespace Application.Queries.Dogs.GetDogBreedAndWeight
{
    public class GetDogByBreedAndWeightQueryValidator : AbstractValidator<GetDogByBreedAndWeightQuery>
    {
        public GetDogByBreedAndWeightQueryValidator()
        {
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("The weight must be greater than 0.");
        }
    }
}