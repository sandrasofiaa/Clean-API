using Application.Queries.Dogs.GetAll;
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

namespace Application.Animals.Queries.Dogs.GetAll
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<GetAllDogsQueryHandler> _logger;

        public GetAllDogsQueryHandler(IAnimalRepository animalRepository, ILogger<GetAllDogsQueryHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<AnimalModel> allAnimals = await _animalRepository.GetAllAsync();

                if (allAnimals == null || !allAnimals.Any())
                {
                    _logger.LogInformation("Inga djur hittades i databasen.");
                    return new List<Dog>(); // Returnera en tom lista om inga djur hittades
                }

                List<Dog> allDogs = allAnimals.OfType<Dog>().ToList();
                return allDogs;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ett fel inträffade vid hämtning av alla katter: {ex.Message}");
                throw; // Kasta vidare undantaget för att hantera det på en högre nivå
            }
        }
    }
}
