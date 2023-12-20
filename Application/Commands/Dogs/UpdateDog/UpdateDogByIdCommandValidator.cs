using Application.Commands.Dogs.AddDog;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandValidator : AbstractValidator<UpdateDogByIdCommand>
    {
        public UpdateDogByIdCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Dog Id must be provided");
            RuleFor(command => command.UpdatedDog.Name).NotEmpty().WithMessage("Dog name must be provided");
            RuleFor(command => command.UpdatedDog.Breed).NotEmpty().WithMessage("Dog breed must be provided");

            RuleFor(command => command.UpdatedDog.Weight)
                .GreaterThan(0).WithMessage("Dog weight must be greater than 0");
        }
    }
}