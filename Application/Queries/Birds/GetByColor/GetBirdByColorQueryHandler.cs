using Domain.Data;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Birds.GetByColor
{
    public class GetBirdByColorQueryHandler : IRequestHandler<GetBirdByColorQuery, List<Bird>>
    {
        private readonly AnimalDbContext _animalDbContext;

        public GetBirdByColorQueryHandler(AnimalDbContext animalDbContext)
        {
            _animalDbContext = animalDbContext;
        }

        public async Task<List<Bird>> Handle(GetBirdByColorQuery request, CancellationToken cancellationToken)
        {
            string upperColor = request.Color.ToUpper();

            var query = _animalDbContext.Birds
                .Where(b => b.Color.ToUpper() == upperColor)
                .OrderByDescending(b => b.Name)
                .ThenByDescending(b => b.AnimalId);

            return await query.ToListAsync(cancellationToken);
        }
    }
}