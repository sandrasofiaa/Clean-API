using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Cats.AddCat
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly MockDatabase _mockDatabase;

        public AddCatCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.NewCat.Name))
            {
                throw new ArgumentException("Cat's name cannot be empty or whitespace");
            }

            Cat CatToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                LikesToPlay = request.NewCat.LikesToPlay
            };

            _mockDatabase.Cats.Add(CatToCreate);

            return Task.FromResult(CatToCreate);
        }
    }
}
