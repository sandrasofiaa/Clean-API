using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Birds.AddBird
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IAnimalRepository _animalRepository;

        public AddBirdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.NewBird.Name))
            {
                throw new ArgumentException("Bird name cannot be empty or whitespace");
            }

            Bird BirdToCreate = new Bird
            {
                AnimalId = Guid.NewGuid(),
                Name = request.NewBird.Name
            };

            try
            {
                await _animalRepository.AddAnimalAsync(BirdToCreate);
                return BirdToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Bird to the database", ex);
            }
        }
    }

}