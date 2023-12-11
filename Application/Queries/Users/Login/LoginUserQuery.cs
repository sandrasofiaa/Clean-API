using Application.Dtos;
using MediatR;

namespace Application.Queries.Users.Login
{
    public class LoginUserQuery : IRequest<string>
    {
        public LoginUserQuery(UserRegistrationDto loginUser)
        {
            LoginUser = loginUser;
        }

        public UserRegistrationDto LoginUser { get; }
    }
}