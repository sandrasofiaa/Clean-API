using Application.Commands.Cats.UpdateCat;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;
using FluentValidation;

public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly UpdateCatByIdCommandValidator _validator;

    public UpdateCatByIdCommandHandler(IAnimalRepository animalRepository, UpdateCatByIdCommandValidator validator)
    {
        _animalRepository = animalRepository;
        _validator = validator;
    }

    public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            // Fetch the cat to update from the repository using a more generic method
            Cat catToUpdate = (Cat)await _animalRepository.GetByIdAsync(request.Id);

            if (catToUpdate != null)
            {
                // Update the properties of the cat
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.Breed = request.UpdatedCat.Breed; // Uppdatera rasen
                catToUpdate.Weight = (int)request.UpdatedCat.Weight; // Uppdatera vikten

                // Call your repository method to update the cat in the database
                await _animalRepository.UpdateAnimalAsync(catToUpdate);

                // Return the updated cat
                return catToUpdate;
            }

            // Handle if the cat is not found
            return null; // or throw an exception
        }
        catch (ValidationException)
        {
            throw; // Låt FluentValidation.ValidationException gå igenom
        }
    }
}