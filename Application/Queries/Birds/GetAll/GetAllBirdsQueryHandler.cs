using Application.Queries.Birds.GetAll;
using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Database;
using Infrastructure.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Birds
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<GetAllBirdsQueryHandler> _logger;

        public GetAllBirdsQueryHandler(IAnimalRepository animalRepository, ILogger<GetAllBirdsQueryHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<AnimalModel> allAnimals = await _animalRepository.GetAllAsync();

                if (allAnimals == null || !allAnimals.Any())
                {
                    _logger.LogInformation("Inga djur hittades i databasen.");
                    return new List<Bird>(); // Returnera en tom lista om inga djur hittades
                }

                List<Bird> allBirds = allAnimals.OfType<Bird>().ToList();
                return allBirds;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ett fel inträffade vid hämtning av alla katter: {ex.Message}");
                throw; // Kasta vidare undantaget för att hantera det på en högre nivå
            }

        }
    }
}
