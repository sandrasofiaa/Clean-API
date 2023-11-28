using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommand : IRequest<bool>
    {
        public DeleteDogByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

