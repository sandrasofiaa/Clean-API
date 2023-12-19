using Application.Commands.Dogs.DeleteDog;
using AutoFixture.NUnit3;
using Domain.Models;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogByIdCommandHandlerTests
    {
        private Mock<IAnimalRepository> _animalRepositoryMock;
        private DeleteDogByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new DeleteDogByIdCommandHandler(_animalRepositoryMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task WHEN_Handle_THEN_DeleteDog_return_true([Frozen] Dog initialDog)
        {
            // Arrange
            _animalRepositoryMock.Setup(x => x.GetByIdAsync(initialDog.AnimalId)).ReturnsAsync(initialDog);

            // Act
            var command = new DeleteDogByIdCommand(initialDog.AnimalId);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
