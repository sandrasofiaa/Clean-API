using Application.Commands.Users;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.CommandHandlers.Users
{
    public class AddNewAnimalCommandHandler : IRequestHandler<AddNewAnimalCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public AddNewAnimalCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddNewAnimalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newAnimalDto = request.NewAnimalToUser;

                // Här kan du fortfarande göra eventuell anpassning eller validering av inkommande data

                // Konvertera från UserAnimalDto till UserAnimal-modell
                var userAnimal = new UserAnimal
                {
                    UserId = newAnimalDto.UserId,
                    AnimalId = newAnimalDto.AnimalId,
                    // Andra egenskaper som behövs här...
                };

                return await _userRepository.AddUserAnimalAsync(userAnimal);
            }
            catch (Exception ex)
            {
                // Logga andra fel som kan uppstå vid tillägg av djur till användare
                // Log.Error("...", ex);
                return false; // Returnera false för andra typer av exception
            }
        }
    }
}