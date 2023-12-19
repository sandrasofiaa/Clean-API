using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegisterUserCommand(UserRegistrationDto newUser)
        {
            NewUser = newUser;
        }

        public UserRegistrationDto NewUser { get; }
    }
}