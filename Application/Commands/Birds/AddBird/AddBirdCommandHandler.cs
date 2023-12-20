using Domain.Models;
using Infrastructure.Interface; // Lägg till detta namespace för att använda AnimalRepository
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
                // Fyll på med andra egenskaper för din hund om det behövs
            };

            try
            {
                await _animalRepository.AddAnimalAsync(BirdToCreate); // Använd generiska metoden här
                return BirdToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Bird to the database", ex);
            }
        }
    }

}