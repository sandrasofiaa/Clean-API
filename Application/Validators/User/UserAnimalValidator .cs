using Domain.Dtos;
using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.User
{
    public class UserAnimalValidator : AbstractValidator<UserAnimalDto>
    {
        public UserAnimalValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must be less than 50 characters.");

            RuleFor(x => x.AnimalId)
                .NotEmpty().WithMessage("AnimalId is required.");

            RuleFor(x => x.AnimalName)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");
        }
    }
}
