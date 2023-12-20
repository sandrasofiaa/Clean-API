using Application.Commands.Dogs.DeleteDog;
using Application.Validators;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

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
            // Anropa DeleteAsync<Dog> för att ta bort en hund från databasen med request.Id
            await _animalRepository.DeleteAsync<Dog>(request.Id);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}