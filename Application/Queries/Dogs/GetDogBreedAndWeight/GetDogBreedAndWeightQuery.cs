using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Dogs.GetDogBreedAndWeight
{
    public class GetDogByBreedAndWeightQuery : IRequest<List<Dog>>
    {
        public string? Breed { get; set; }
        public int? Weight { get; set; }
    }
}
