using Application.Commands.Dogs.DeleteDog;
using Application.Validators;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteDogByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            var validator = new GuidValidator();
            var validationResult = await validator.ValidateAsync(request.Id);

            if (!validationResult.IsValid)
            {
                return false;
            }

            try
            {
                await _animalRepository.DeleteAsync<Dog>(request.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}