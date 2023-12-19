using Domain.Models;
using Domain.Models.Animal;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Cats.GetAll
{
    public class GetAllCatsQuery : IRequest<List<Cat>>
    {
    }
}