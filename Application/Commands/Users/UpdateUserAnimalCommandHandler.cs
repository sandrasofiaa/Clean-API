using Application.Commands.Users;
using Infrastructure.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Users
{
    public class UpdateUserAnimalHandler : IRequestHandler<UpdateUserAnimalCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserAnimalHandler> _logger;

        public UpdateUserAnimalHandler(IUserRepository userRepository, ILogger<UpdateUserAnimalHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateUserAnimalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.UpdateUserAnimal(request.UserId, request.OldAnimalId, request.NewAnimalId);
                return Unit.Value; // Returnera Unit när uppdateringen är klar
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update user's animal.");
                throw;
            }
        }
    }
}