using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Birds.GetById
{
    public class GetByBirdIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird?>
    {
        private readonly MockDatabase _mockDatabase;

        public GetByBirdIdQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Bird?> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            Bird? wantedBird = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == request.Id);
            return Task.FromResult(wantedBird);
        }
    }
}
