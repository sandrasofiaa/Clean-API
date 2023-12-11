using Application.Queries.Users.Login;
using MediatR;

namespace Application.Handlers.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, string>
    {
        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            // Här kan du hantera autentisering och JWT-generering med användardata från request.LoginUser.Username och request.LoginUser.Password

            // Exempel:
            var username = request.LoginUser.Username;
            var password = request.LoginUser.Password;

            // Utför autentisering av användaren här...

            // Om autentiseringen lyckas, generera JWT-token
            var token = GenerateJwtToken(username);

            // Returnera JWT-token (eller annan relevant data) tillbaka
            return Task.FromResult(token);
        }

        private string GenerateJwtToken(string username)
        {
            // Här kan du implementera logiken för att generera JWT-token baserat på användarnamnet (eller annan data)

            // Detta är en stubb - Ersätt med din faktiska JWT-genereringslogik
            var token = "ExempelJWTTokenFör" + username;

            return token;
        }
    }
}