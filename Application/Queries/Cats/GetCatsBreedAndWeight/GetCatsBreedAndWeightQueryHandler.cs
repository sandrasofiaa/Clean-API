using Application.Queries.Cats.GetCatsByBreedAndWeight;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Queries.Cats.GetPetsByBreedAndWeight
{
    public class GetCatsByBreedAndWeightQueryHandler : IRequestHandler<GetCatsByBreedAndWeightQuery, List<Cat>>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetCatsByBreedAndWeightQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Cat>> Handle(GetCatsByBreedAndWeightQuery request, CancellationToken cancellationToken)
        {
            var result = await _animalRepository.GetCatsByWeightBreedAsync(request.Breed, request.Weight);
            return result;
        }
    }
}