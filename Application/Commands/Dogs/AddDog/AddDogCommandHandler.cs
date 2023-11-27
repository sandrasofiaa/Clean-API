using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly MockDatabase _mockDatabase;

        public AddDogCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.NewDog.Name))
            {
                throw new ArgumentException("Dog name cannot be empty or whitespace");
            }

            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            _mockDatabase.Dogs.Add(dogToCreate);

            return Task.FromResult(dogToCreate);
        }
    }
}
