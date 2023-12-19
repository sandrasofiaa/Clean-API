using Domain.Models;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    internal class GetUsersWithAnimalsQueryHandler : IRequestHandler<GetUsersWithAnimalsQuery, List<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUsersWithAnimalsQueryHandler> _logger;

        public GetUsersWithAnimalsQueryHandler(IUserRepository userRepository, ILogger<GetUsersWithAnimalsQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<List<object>> Handle(GetUsersWithAnimalsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserAnimal> usersWithAnimals = await _userRepository.GetAllUsersWithAnimals();

                var usersWithAllTheirAnimals = usersWithAnimals
                    .GroupBy(u => u.UserName)
                    .Select(g => new
                    {
                        UserName = g.Key,
                        Animals = g.Select(userAnimal => new
                        {
                            userAnimal.UserId,
                            userAnimal.UserName,
                            userAnimal.AnimalId,
                            userAnimal.Name
                            // Lägg till andra önskade egenskaper här
                        }).ToList()
                    })
                    .ToList<object>(); // Använd en generisk typ istället för 'object' om det finns en specifik typ att använda

                return usersWithAllTheirAnimals.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching users with animals", ex);
                throw; // Du kan hantera felet på ett mer passande sätt här beroende på ditt användningsfall
            }
        }
    }
}
