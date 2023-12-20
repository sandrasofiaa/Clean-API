using Application.Queries.Dogs.GetDogBreedAndWeight;
using Domain.Data;
using Domain.Models;
using FluentValidation;
using Infrastructure.Interface;
using MediatR;

namespace Application.Queries.Dogs.GetDogByBreedAndWeight
{
    public class GetDogBreedAndWeightQueryHandler : IRequestHandler<GetDogByBreedAndWeightQuery, List<Dog>>
    {
        private readonly AnimalDbContext _animalDbContext;
        private readonly IAnimalRepository _animalRepository;

        public GetDogBreedAndWeightQueryHandler(AnimalDbContext animalDbContext, IAnimalRepository dogService)
        {
            _animalDbContext = animalDbContext;
            _animalRepository = dogService;
        }

        public async Task<List<Dog>> Handle(GetDogByBreedAndWeightQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetDogByBreedAndWeightQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var dogsByWeightBreed = await _animalRepository.GetDogsByWeightBreedAsync(request.Breed, request.Weight);

            return dogsByWeightBreed;
        }
    }
}