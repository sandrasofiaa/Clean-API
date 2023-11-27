using Domain.Models;
using Infrastructure.Database;
using MediatR;


namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteBirdByIdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            Bird birdToDelete = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == request.Id);

            if (birdToDelete != null)
            {
                _mockDatabase.Birds.Remove(birdToDelete);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
