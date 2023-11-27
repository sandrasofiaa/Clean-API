using Application.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdByIdCommand : IRequest<bool>
    {
        public DeleteBirdByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}

