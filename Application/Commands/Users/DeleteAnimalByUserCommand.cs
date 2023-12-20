using MediatR;

namespace Application.Commands.Users
{
    public class DeleteAnimalByUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; }
        public Guid AnimalId { get; }

        public DeleteAnimalByUserCommand(Guid userId, Guid animalId)
        {
            UserId = userId;
            AnimalId = animalId;
        }
    }
}