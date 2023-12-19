using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Common.Birds
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
            // Fetch the Bird to update from the repository using a more generic method
            Bird birdToUpdate = (Bird)await _animalRepository.GetByIdAsync(request.Id);

            if (birdToUpdate != null)
            {
                // Update the properties of the bird
                birdToUpdate.Name = request.UpdatedBird.Name;

                // Call your repository method to update the bird in the database
                await _animalRepository.UpdateAnimalAsync(birdToUpdate);

                // Return the updated bird
                return birdToUpdate;
            }

            // Handle if the bird is not found
            return null; // or throw an exception
        }
    }
}
