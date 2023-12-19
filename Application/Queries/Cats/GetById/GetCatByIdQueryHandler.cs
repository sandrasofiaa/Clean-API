using Application.Queries.Cats.GetById; // Update the namespace
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Animals.Queries.Cats.GetById // Update the namespace
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat> // Update class and request types
    {
        private readonly IAnimalRepository _animalRepository; // Change repository interface

        public GetCatByIdQueryHandler(IAnimalRepository catRepository) // Change constructor parameter
        {
            _animalRepository = catRepository; // Update repository assignment
        }

        public async Task<Cat?> Handle(GetCatByIdQuery request, CancellationToken cancellationToken) // Update return type and request type
        {
            Cat? wantedCat = await _animalRepository.GetByIdAsync(request.Id) as Cat;

            return wantedCat;
        }
    }
}