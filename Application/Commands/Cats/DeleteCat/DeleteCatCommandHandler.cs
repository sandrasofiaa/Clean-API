using Domain.Models;
using Infrastructure.Interface; // Byt ut mot din faktiska namespace
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteCatByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Anropa DeleteAsync-metoden från _animalRepository och skicka med request.Id
                await _animalRepository.DeleteAsync<Cat>(request.Id);
                return true;
            }
            catch (Exception)
            {
                // Hantera undantag om det behövs
                return false;
            }
        }
    }
}