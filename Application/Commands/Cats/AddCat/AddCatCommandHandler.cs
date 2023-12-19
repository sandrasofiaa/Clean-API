using Domain.Models;
using Infrastructure.Interface;
using MediatR;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Commands.Cats.AddCat;

namespace Application.Commands.Cats
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IValidator<CatDto> _validator;

        public AddCatCommandHandler(IAnimalRepository animalRepository, IValidator<CatDto> validator)
        {
            _animalRepository = animalRepository;
            _validator = validator;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            var catDto = request.NewCat;

            // Perform validation
            var validationResult = await _validator.ValidateAsync(catDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                // Handle validation errors here, perhaps throw an exception
                throw new ValidationException(validationResult.Errors);
            }

            Cat catToCreate = new Cat
            {
                AnimalId = Guid.NewGuid(),
                Name = catDto.Name,
                Breed = catDto.Breed,
                Weight = (int)catDto.Weight,
                LikesToPlay = catDto.LikesToPlay
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