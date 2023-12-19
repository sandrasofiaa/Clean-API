using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class UpdateUserAnimalDtoValidator : AbstractValidator<UpdateUserAnimalDto>
    {
        public UpdateUserAnimalDtoValidator()
        {
            RuleFor(dto => dto.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId cannot be empty.");

            RuleFor(dto => dto.OldAnimalId)
                .NotEmpty().WithMessage("OldAnimalId is required.")
                .NotEqual(Guid.Empty).WithMessage("OldAnimalId cannot be empty.");

            RuleFor(dto => dto.NewAnimalId)
                .NotEmpty().WithMessage("NewAnimalId is required.")
                .NotEqual(Guid.Empty).WithMessage("NewAnimalId cannot be empty.")
                .NotEqual(dto => dto.OldAnimalId).WithMessage("NewAnimalId must be different from OldAnimalId.");
        }
    }
}
