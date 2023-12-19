﻿using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat> //Updated class name and return type
    {
        private readonly IAnimalRepository _animalRepository;

        public UpdateCatByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken) //Updated method signature
        {
            var catDto = request.UpdatedCat; //Assuming UpdatedCat is of type CatDto

            try
            {
                //Fetch the cat to update from the repository using a more generic method
                Cat catToUpdate = (Cat)await _animalRepository.GetByIdAsync(request.Id); //Updated variable names

                if (catToUpdate != null)
                {
                    //Update the properties of the cat
                    catToUpdate.Name = catDto.Name;
                    catToUpdate.Breed = catDto.Breed;
                    catToUpdate.Weight = (int)catDto.Weight;

                    //Call your repository method to update the cat in the database
                    await _animalRepository.UpdateAnimalAsync(catToUpdate);

                    //Return the updated cat
                    return catToUpdate;
                }

                // Handle if the cat is not found
                return null; //or throw an exception
            }
            catch (Exception ex)
            {
                //Handle exceptions
                throw;
            }
        }
    }
}