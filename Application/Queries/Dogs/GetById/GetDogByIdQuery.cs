using Application.Validators;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQuery : IRequest<Dog>
    {
        public GetDogByIdQuery(Guid dogId)
        {
            Id = dogId;
        }

        public Guid Id { get; }

        //public void Validate()
        //{
        //    var validator = new GuidValidator();
        //    var result = validator.Validate(Id);

        //    if (!result.IsValid)
        //    {
        //        throw new ValidationException(result.Errors);
        //    }
        //}
    }
}
