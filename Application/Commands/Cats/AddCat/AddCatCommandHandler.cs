using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Application.Validators.CatValidator; 
using Domain.Models;
using FluentValidation;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Cats 
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat> 
    {
        private readonly IAnimalRepository _animalRepository;

        public AddCatCommandHandler(IAnimalRepository animalRepository, IValidator<CatDto> validator) 
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken) 
        {
            var catDto = request.NewCat; 

            // Perform validation using CatValidator
            var validationResult = await new CatValidator().ValidateAsync(catDto, cancellationToken); 
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Cat catToCreate = new Cat
            {
                AnimalId = Guid.NewGuid(),
                Name = catDto.Name,
                Breed = catDto.Breed,
                Weight = (int)catDto.Weight
            };

            try
            {
                await _animalRepository.AddAnimalAsync(catToCreate);
                return catToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add cat to the database", ex);
            }
        }
    }
}