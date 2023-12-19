using MediatR;

namespace Application.Commands.Users
{
    public class UpdateUserAnimalCommand : IRequest<Unit>
    {
        public Guid UserId { get; }
        public Guid OldAnimalId { get; }
        public Guid NewAnimalId { get; }

        public UpdateUserAnimalCommand(Guid userId, Guid oldAnimalId, Guid newAnimalId)
        {
            UserId = userId;
            OldAnimalId = oldAnimalId;
            NewAnimalId = newAnimalId;
        }
    }
}