using Domain.Dtos;
using MediatR;

namespace Application.Commands.Users
{
    public class AddNewAnimalCommand : IRequest<bool>
    {
        public AddNewAnimalCommand(UserAnimalDto newAnimalToUser)
        {
            NewAnimalToUser = newAnimalToUser;
        }

        public UserAnimalDto NewAnimalToUser { get; }

    }
}