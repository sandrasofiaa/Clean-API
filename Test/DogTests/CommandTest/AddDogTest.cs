using Application.Commands.Dogs;
using Application.Dtos;
using AutoFixture.NUnit3;
using Domain.Models;
using FluentAssertions;
using FluentValidation;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.DogTests.CommandTest

{
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        private AddDogCommandHandler _handler;
        private Mock<IAnimalRepository> _animalRepositoryMock;
        private Mock<IValidator<DogDto>> _validatorMock;

        [SetUp]
        public void SetUp()
        {
            _validatorMock = new Mock<IValidator<DogDto>>();
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new AddDogCommandHandler(_animalRepositoryMock.Object, _validatorMock.Object);

        }

        [Test]
        [CustomAutoData]
        public async Task Handle_ValidDog_ReturnsCreatedDog([Frozen] DogDto dogDto)
        {
            // Arrange

            var command = new AddDogCommand(dogDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dogDto.Name);
            result.Breed.Should().Be(dogDto.Breed);
            result.Weight.Should().Be((int)dogDto.Weight);
            _animalRepositoryMock.Verify(x => x.AddAnimalAsync(It.IsAny<Dog>()), Times.Once);
        }
    }
}
