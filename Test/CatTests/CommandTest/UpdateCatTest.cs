using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using AutoFixture.NUnit3;
using Domain.Models;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class UpdateCatsTest
    {
        private UpdateCatByIdCommandHandler _handler;
        private Mock<IAnimalRepository> _animalRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new UpdateCatByIdCommandHandler(_animalRepositoryMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task UpdateCatByIdHandler_UpdatesCatCorrectly([Frozen] Cat initialCat, CatDto updatedCat)
        {
            // Arrange
            _animalRepositoryMock.Setup(x => x.GetByIdAsync(initialCat.AnimalId)).ReturnsAsync(initialCat);

            var command = new UpdateCatByIdCommand(updatedCat, initialCat.AnimalId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Cat>());
        }
    }
}
