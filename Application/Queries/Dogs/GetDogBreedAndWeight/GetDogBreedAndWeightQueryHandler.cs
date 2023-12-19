using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Interface;
using Domain.Data;
using Application.Queries.Dogs.GetDogBreedAndWeight; // Updated namespace


using Application.Validators.DogValidator; // Updated namespace

namespace Application.Queries.Dogs.GetDogByBreedAndWeight // Updated namespace and class name
{
    public class GetDogBreedAndWeightQueryHandler : IRequestHandler<GetDogByBreedAndWeightQuery, List<Dog>> // Updated class and request types
    {
        private readonly AnimalDbContext _animalDbContext;
        private readonly IAnimalRepository _animalRepository;

        public GetDogBreedAndWeightQueryHandler(AnimalDbContext animalDbContext, IAnimalRepository dogService) // Updated parameter name
        {
            _animalDbContext = animalDbContext;
            _animalRepository = dogService;
        }

        public async Task<List<Dog>> Handle(GetDogByBreedAndWeightQuery request, CancellationToken cancellationToken) // Updated request type
        {
            var validator = new GetDogByBreedAndWeightQueryValidator(); // Updated validator name
            var validationResult = await validator.ValidateAsync(request, cancellationToken); // Validates the request

            if (!validationResult.IsValid)
            {
                // If validation fails, throw a ValidationException with error messages
                throw new ValidationException(validationResult.Errors);
            }

            // If validation succeeds, retrieve dogs by breed and weight
            var dogsByWeightBreed = await _animalRepository.GetDogsByWeightBreedAsync(request.Breed, request.Weight); // Updated method name

            return dogsByWeightBreed;
        }
    }
}