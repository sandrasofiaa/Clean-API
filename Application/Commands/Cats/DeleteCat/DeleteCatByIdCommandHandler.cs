using Application.Commands.Cats.DeleteCat;
using Application.Validators;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
{
    private readonly IAnimalRepository _animalRepository;

    public DeleteCatByIdCommandHandler(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
    {
        var validator = new GuidValidator();
        var validationResult = await validator.ValidateAsync(request.Id);

        if (!validationResult.IsValid)
        {
            return false;
        }

        try
        {
            // Call DeleteAsync<Cat> to remove a cat from the database using request.Id
            await _animalRepository.DeleteAsync<Cat>(request.Id);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}