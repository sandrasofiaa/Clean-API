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

                var userAnimal = new UserAnimal
                {
                    UserId = newAnimalDto.UserId,
                    AnimalId = newAnimalDto.AnimalId,
                };

                return await _userRepository.AddUserAnimalAsync(userAnimal);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}