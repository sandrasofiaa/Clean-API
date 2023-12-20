using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Animals.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetDogByIdQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Dog?> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog? wantedDog = await _animalRepository.GetByIdAsync(request.Id) as Dog;

            return wantedDog;
        }
    }
}