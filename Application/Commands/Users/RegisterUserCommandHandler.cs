using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newUserDto = request.NewUser;

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUserDto.Password);

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = newUserDto.Username,
                    UserPassword = hashedPassword,
                    // Inkludera andra användarrelaterade egenskaper om det behövs
                };

                await _userRepository.RegisterUser(newUser, newUserDto.Password);

                // Ta bort lösenordet innan du returnerar användaren
                newUser.UserPassword = "*************";

                return newUser;
            }
            catch (Exception ex)
            {
                // Hantera exceptioner på lämpligt sätt
                throw;
            }
        }

    }
}