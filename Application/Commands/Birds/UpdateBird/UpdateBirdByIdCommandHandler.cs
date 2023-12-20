using Application.Common.Birds;
using Application.Validators;
using Application.Validators.BirdValidator;
using Domain.Models;
using FluentValidation;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    namespace Application.Commands.Birds.UpdateBird
    {
        public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
        {
            private readonly IAnimalRepository _animalRepository;

            public UpdateBirdByIdCommandHandler(IAnimalRepository animalRepository)
            {
                _animalRepository = animalRepository;
            }

            public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
            {
                var birdDto = request.UpdatedBird; // Assuming UpdatedBird is of type BirdDto

                try
                {
                    // Fetch the bird to update from the repository using a more generic method
                    Bird birdToUpdate = (Bird)await _animalRepository.GetByIdAsync(request.Id);

                    if (birdToUpdate != null)
                    {
                        // Update the properties of the bird
                        birdToUpdate.Name = birdDto.Name;
                        birdToUpdate.CanFly = birdDto.CanFly;

                        // Call your repository method to update the bird in the database
                        await _animalRepository.UpdateAnimalAsync(birdToUpdate);

                        // Return the updated bird
                        return birdToUpdate;
                    }

                    // Handle if the bird is not found
                    return null; // or throw an exception
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    throw;
                }
            }
        }
    }
}
