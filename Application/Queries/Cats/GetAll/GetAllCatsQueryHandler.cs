using Application.Queries.Cats.GetAll;
using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Animals.Queries.Cats.GetAll
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<GetAllCatsQueryHandler> _logger;

        public GetAllCatsQueryHandler(IAnimalRepository animalRepository, ILogger<GetAllCatsQueryHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<AnimalModel> allAnimals = await _animalRepository.GetAllAsync();

                if (allAnimals == null || !allAnimals.Any())
                {
                    _logger.LogInformation("Inga djur hittades i databasen.");
                    return new List<Cat>(); // Returnera en tom lista om inga djur hittades
                }

                List<Cat> allCats = allAnimals.OfType<Cat>().ToList();
                return allCats;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ett fel inträffade vid hämtning av alla katter: {ex.Message}");
                throw; // Kasta vidare undantaget för att hantera det på en högre nivå
            }
        }
    }
}
