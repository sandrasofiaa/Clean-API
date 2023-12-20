using Application.Commands.Cats;
using Application.Commands.Cats.AddCat;
using Application.Dtos;
using AutoFixture.NUnit3;
using Domain.Models;
using FluentAssertions;
using FluentValidation;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class AddCatCommandHandlerTests
    {
        private AddCatCommandHandler _handler;
        private Mock<IAnimalRepository> _animalRepositoryMock;
        private Mock<IValidator<CatDto>> _validatorMock;

        [SetUp]
        public void SetUp()
        {
            _validatorMock = new Mock<IValidator<CatDto>>();
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new AddCatCommandHandler(_animalRepositoryMock.Object, _validatorMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task AddCatCommandHandler_CreatesValidCat_ReturnsCat([Frozen] CatDto catDto)
        {
            // Arrange
            var command = new AddCatCommand(catDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(catDto.Name);
            result.Breed.Should().Be(catDto.Breed);
            result.Weight.Should().Be((int)catDto.Weight);
            _animalRepositoryMock.Verify(x => x.AddAnimalAsync(It.IsAny<Cat>()), Times.Once);
        }
    }
}