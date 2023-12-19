using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Cats.GetCatsByBreedAndWeight
{
    public class GetCatsByBreedAndWeightQuery : IRequest<List<Cat>>
    {
        public string Breed { get; set; }
        public int? Weight { get; set; }
    }
}
