using MediatR;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommand : IRequest<bool>
    {
        public DeleteCatByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

