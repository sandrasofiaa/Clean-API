using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandValidator : AbstractValidator<UpdateCatByIdCommand>
    {
        public UpdateCatByIdCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Cat Id must be provided");
            RuleFor(command => command.UpdatedCat.Name).NotEmpty().WithMessage("Cat name must be provided");
            RuleFor(command => command.UpdatedCat.Breed).NotEmpty().WithMessage("Cat breed must be provided");

            RuleFor(command => command.UpdatedCat.Weight)
                .GreaterThan(0).WithMessage("Cat weight must be greater than 0");
        }
    }
}