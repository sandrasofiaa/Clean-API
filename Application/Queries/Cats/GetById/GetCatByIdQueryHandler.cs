using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Birds.GetById
{
    public class GetCatBydIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat?>
    {
        private readonly MockDatabase _mockDatabase;

        public GetCatBydIdQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat?> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            Cat? wantedCat = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.Id);
            return Task.FromResult(wantedCat);
        }
    }
}
