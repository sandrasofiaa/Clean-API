using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly MockDatabase _mockDatabase;

        public RegisterUserCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Validate the request (e.g., check for required fields)

            // Check if the username is unique in the mock database
            var isUsernameUnique = !_mockDatabase.Users.Any(u => u.UserName == request.NewUser.Username);
            if (!isUsernameUnique)
            {
                throw new InvalidOperationException("Username is already taken");
            }

            // Hash the password before storing it (you need to implement PasswordHasher)
            // var hashedPassword = PasswordHasher.HashPassword(request.NewUser.Password);

            // Create a new user entity with the registration data
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.Username,
                UserPassword = request.NewUser.Password,
                // Include other user-related properties as needed
            };

            // Save the new user to the repository
            _mockDatabase.Users.Add(newUser);

            return Task.FromResult(newUser);
        }
    }
}

