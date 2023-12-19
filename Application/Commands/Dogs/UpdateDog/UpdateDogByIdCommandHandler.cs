using Application.Validators;
using Application.Validators.DogValidator;
using Domain.Models;
using FluentValidation;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    namespace Application.Commands.Dogs.UpdateDog
    {
        public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
        {
            private readonly IAnimalRepository _animalRepository;

            public UpdateDogByIdCommandHandler(IAnimalRepository animalRepository)
            {
                _animalRepository = animalRepository;
            }

            public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
            {
                var dogDto = request.UpdatedDog; // Assuming UpdatedDog is of type DogDto

                try
                {
                    // Fetch the dog to update from the repository using a more generic method
                    Dog dogToUpdate = (Dog)await _animalRepository.GetByIdAsync(request.Id);

                    if (dogToUpdate != null)
                    {
                        // Update the properties of the dog
                        dogToUpdate.Name = dogDto.Name;
                        dogToUpdate.Breed = dogDto.Breed;
                        dogToUpdate.Weight = (int)dogDto.Weight;

                        // Call your repository method to update the dog in the database
                        await _animalRepository.UpdateAnimalAsync(dogToUpdate);

                        // Return the updated dog
                        return dogToUpdate;
                    }

                    // Handle if the dog is not found
                    return null; // or throw an exception
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    throw ex;
                }
            }
        }
        //public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
        //{
        //    private readonly IAnimalRepository _animalRepository;
        //    private readonly DogValidator _dogValidator;
        //    private readonly GuidValidator _guidValidator;

        //    public UpdateDogByIdCommandHandler(IAnimalRepository animalRepository, DogValidator dogValidator, GuidValidator guidValidator)
        //    {
        //        _animalRepository = animalRepository;
        //        _dogValidator = dogValidator;
        //        _guidValidator = guidValidator;
        //    }

        //    public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        //    {
        //        var dogDto = request.UpdatedDog; // Assuming UpdatedDog is of type DogDto

        //        var dogValidationResult = _dogValidator.Validate(dogDto);
        //        var guidValidationResult = _guidValidator.Validate(request.Id);

        //        if (!dogValidationResult.IsValid || !guidValidationResult.IsValid)
        //        {
        //            // Handle validation errors
        //            var errorMessages = dogValidationResult.Errors
        //                .Concat(guidValidationResult.Errors)
        //                .Select(error => error.ErrorMessage);

        //            // For example, throwing a custom exception with validation error messages
        //            throw new ValidationException(string.Join(", ", errorMessages));
        //        }

        //        try
        //        {
        //            // Fetch the dog to update from the repository using a more generic method
        //            Dog dogToUpdate = (Dog)await _animalRepository.GetByIdAsync(request.Id);

        //            if (dogToUpdate != null)
        //            {
        //                // Update the properties of the dog
        //                dogToUpdate.Name = dogDto.Name;
        //                dogToUpdate.Breed = dogDto.Breed;
        //                dogToUpdate.Weight = (int)dogDto.Weight;

        //                // Call your repository method to update the dog in the database
        //                await _animalRepository.UpdateAnimalAsync(dogToUpdate);

        //                // Return the updated dog
        //                return dogToUpdate;
        //            }

        //            // Handle if the dog is not found
        //            return null; // or throw an exception
        //        }
        //        catch (ValidationException)
        //        {
        //            throw;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle other exceptions
        //            throw ex;
        //        }
        //    }
    }
}