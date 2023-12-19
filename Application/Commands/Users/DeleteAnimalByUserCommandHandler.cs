﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users;
using Infrastructure.Interface;

namespace Application.Handlers.Users
{
    public class DeleteAnimalByUserHandler : IRequestHandler<DeleteAnimalByUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteAnimalByUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(DeleteAnimalByUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.DeleteAnimalByUser(request.UserId, request.AnimalId);
                // Lägg till logik för lyckad borttagning här om det behövs
                return Unit.Value; // Returnera Unit när borttagningen är klar
            }
            catch (Exception ex)
            {
                // Hantera fel om något går fel vid borttagningen av djuret från användaren
                throw new ApplicationException("Failed to delete animal from user.", ex);
            }
        }
    }
}
