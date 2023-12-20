using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Interface;
using MediatR;

namespace Application.Animals.Queries.Cats.GetById
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetCatByIdQueryHandler(IAnimalRepository catRepository)
        {
            _animalRepository = catRepository;
        }

        public async Task<Cat?> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            Cat? wantedCat = await _animalRepository.GetByIdAsync(request.Id) as Cat;

            return wantedCat;
        }
    }
}