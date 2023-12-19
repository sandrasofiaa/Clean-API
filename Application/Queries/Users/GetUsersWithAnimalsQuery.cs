using Domain.Models;
using MediatR;

namespace Application.Queries.Users
{
    public class GetUsersWithAnimalsQuery : IRequest<List<object>>
    {
    }
}
