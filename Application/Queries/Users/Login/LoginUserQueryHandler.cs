using Application.Queries.Users.Login;
using Infrastructure.Interface;
using MediatR;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IUserRepository _userRepository;

    public LoginUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var username = request.LoginUser.Username;
        var password = request.LoginUser.Password;

        // Authenticate user using repository method
        var user = await _userRepository.AuthenticateUserAsync(username, password);

        if (user != null)
        {
            // If authentication succeeds, generate JWT token
            var token = GenerateJwtToken(username);
            return token;
        }

        // Return null or throw an exception for failed authentication
        return null;
    }

    private string GenerateJwtToken(string username)
    {
        // Implement logic to generate JWT token here based on the username
        // ...

        var token = "SecretKey" + username;

        return token;
    }
}