using Application.Commands.Birds.DeleteBird;
using Application.Validators;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteBirdByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            var validator = new GuidValidator();
            var validationResult = await validator.ValidateAsync(request.Id);

            if (!validationResult.IsValid)
            {
                return false;
            }

            try
            {
                // Anropa DeleteAsync<Bird> för att ta bort en fågel från databasen med request.Id
                await _animalRepository.DeleteAsync<Bird>(request.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}