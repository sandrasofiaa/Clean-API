using Application.Dtos;
using Application.Validators.DogValidator;
using Domain.Models;
using FluentValidation;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Dogs.AddDog
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IAnimalRepository _animalRepository;

        public AddDogCommandHandler(IAnimalRepository animalRepository, IValidator<DogDto> validator)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            var dogDto = request.NewDog;

            // Perform validation using DogValidator
            var validationResult = await new DogValidator().ValidateAsync(dogDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Dog dogToCreate = new Dog
            {
                AnimalId = Guid.NewGuid(),
                Name = dogDto.Name,
                Breed = dogDto.Breed,
                Weight = (int)dogDto.Weight
            };

            try
            {
                await _animalRepository.AddAnimalAsync(dogToCreate);
                return dogToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add dog to the database", ex);
            }
        }
    }
}