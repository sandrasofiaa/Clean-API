using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Interface;
using MediatR;

namespace Application.Queries.Birds.GetById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetBirdByIdQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            AnimalModel animalModel = await _animalRepository.GetByIdAsync(request.Id);

            return new Bird
            {
                AnimalId = animalModel.AnimalId,
                Name = animalModel.Name,
            };
        }
    }
}