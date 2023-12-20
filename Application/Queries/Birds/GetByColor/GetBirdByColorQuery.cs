using Domain.Models;
using MediatR;

namespace Application.Queries.Birds.GetByColor
{
    public class GetBirdByColorQuery : IRequest<List<Bird>>
    {
        public string Color { get; }

        public GetBirdByColorQuery(string color)
        {
            Color = color;
        }
    }
}